using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class UserHandler : IHandler<UserContract, string>
    {
        private readonly IdentityDbContext _identityContext;

        public UserHandler(IdentityDbContext identityDbContext)
        {
            _identityContext = identityDbContext;
        }

        public async Task<ListDto<UserContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            var list = await _identityContext.Users
                .Skip(page * pageSize)
                .Take(pageSize)
                .ProjectToType<UserContract>()
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            var userIds = list.Select(x => x.Id);
            var userRoles = await _identityContext.UserRoles
                .Where(x => userIds.Contains(x.UserId))
                .ToListAsync(cancel)
                .ConfigureAwait(false);
            var userClaims = await _identityContext.UserClaims
                .Where(x => userIds.Contains(x.UserId))
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            return new ListDto<UserContract>()
            {
                Items = list.ConvertAll(x =>
                {
                    x.UserRoles = userRoles
                        .Where(y => y.UserId == x.Id)
                        .Select(x => x.RoleId)
                        .ToList();
                    x.UserClaims = userClaims
                        .Where(y => y.UserId == x.Id)
                        .Select(x => x.Adapt<UserClaimsContract>())
                        .ToList();
                    return x;
                }),
                Page = page,
                PageSize = pageSize,
                TotalItems = await _identityContext.Users
                    .CountAsync(cancel)
                    .ConfigureAwait(false)
            };
        }

        public async Task<UserContract> Get(string id, CancellationToken cancel)
        {
            var user = await _identityContext.Users
                .ProjectToType<UserContract>()
                .FirstOrDefaultAsync(x => x.Id == id, cancel)
                .ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                user.UserRoles = await _identityContext.UserRoles
                    .Where(x => x.UserId == user.Id)
                    .Select(x => x.RoleId)
                    .ToListAsync(cancel)
                    .ConfigureAwait(false);

                user.UserClaims = await _identityContext.UserClaims
                    .Where(x => x.UserId == user.Id)
                    .ProjectToType<UserClaimsContract>()
                    .ToListAsync(cancel)
                    .ConfigureAwait(false);
            }

            return user;
        }

        public async Task<ListDto<UserContract>> Delete(string id, int page, int pageSize, CancellationToken cancel)
        {
            var user = await _identityContext.Users
                .FirstOrDefaultAsync(x => x.Id == id, cancel)
                .ConfigureAwait(false);

            var userRoles = await _identityContext.UserRoles
                .Where(x => x.UserId == id)
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            var userClaims = await _identityContext.UserClaims
                .Where(x => x.UserId == id)
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            _identityContext.Users.Remove(user);
            _identityContext.UserRoles.RemoveRange(userRoles);
            _identityContext.UserClaims.RemoveRange(userClaims);

            await _identityContext.SaveChangesAsync(cancel).ConfigureAwait(false);

            return await GetAll(page, pageSize, cancel).ConfigureAwait(false);
        }

        public async Task<UserContract> Create(UserContract dto, CancellationToken cancel)
        {
            var user = new IdentityUser();
            dto.Id = user.Id;
            dto.Adapt(user);

            await _identityContext.Users.AddAsync(user, cancel).ConfigureAwait(false);

            await UpdateRoles(dto, cancel, new List<IdentityUserRole<string>>()).ConfigureAwait(false);
            await UpdateClaims(dto, cancel, new List<IdentityUserClaim<string>>(), user.Id).ConfigureAwait(false);

            await _identityContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return await Get(dto.Id, cancel).ConfigureAwait(false);
        }

        public async Task<UserContract> Update(UserContract dto, CancellationToken cancel)
        {
            var user = await _identityContext.Users
                .FirstOrDefaultAsync(x => x.Id == dto.Id, cancel)
                .ConfigureAwait(false);

            var userRoles = await _identityContext.UserRoles
                .Where(x => x.UserId == dto.Id)
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            var userClaims = await _identityContext.UserClaims
                .Where(x => x.UserId == dto.Id)
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            dto.Adapt(user);
             _identityContext.Users.Update(user);

            await UpdateRoles(dto, cancel, userRoles).ConfigureAwait(false);
            await UpdateClaims(dto, cancel, userClaims, user.Id).ConfigureAwait(false);

            await _identityContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return await Get(dto.Id, cancel).ConfigureAwait(false);
        }

        private async Task UpdateRoles(UserContract dto, CancellationToken cancel,
            IReadOnlyCollection<IdentityUserRole<string>> userRoles)
        {
            
            var rolesToAdd = dto.UserRoles
                .Where(item => item is not null)
                .Where(item => userRoles.All(x => x.RoleId != item))
                .Select(item => new IdentityUserRole<string> {RoleId = item, UserId = dto.Id})
                .ToList();
            await _identityContext.UserRoles.AddRangeAsync(rolesToAdd, cancel).ConfigureAwait(false);

            var rolesToRemove = userRoles
                .Where(item => !dto.UserRoles.Contains(item.RoleId))
                .ToList();
            _identityContext.UserRoles.RemoveRange(rolesToRemove);
        }

        private async Task UpdateClaims(UserContract dto, CancellationToken cancel,
            IReadOnlyCollection<IdentityUserClaim<string>> userClaims, string userId)
        {
            var claimsToRemove = new List<IdentityUserClaim<string>>();
            var claimsToUpdate = new List<IdentityUserClaim<string>>();
            foreach (var item in userClaims)
            {
                var claim = dto.UserClaims.FirstOrDefault(x => x.Id != item.Id);
                if (claim is null)
                {
                    claimsToRemove.Add(item);
                }
                else
                {
                    item.Adapt(claim);
                    claimsToUpdate.Add(item);
                }
            }

            _identityContext.UserClaims.RemoveRange(claimsToRemove);
            _identityContext.UserClaims.UpdateRange(claimsToUpdate);

            var claimsToAdd = dto.UserClaims
                .Where(item => userClaims.All(x => x.Id != item.Id))
                .Select(item =>
                {
                    var claim = new IdentityUserClaim<string>();
                     item.Adapt(claim);
                     claim.UserId = userId;
                    return claim;
                })
                .ToList();
            await _identityContext.UserClaims
                .AddRangeAsync(claimsToAdd, cancel)
                .ConfigureAwait(false);
        }
    }
}