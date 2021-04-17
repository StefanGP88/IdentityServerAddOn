using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class UserHandler : IHandler<UserContract, string>
    {

        private readonly IdentityDbContext _identityContext;

        private readonly TestManager _userManger;

        
        public UserHandler(IdentityDbContext identityDbContext, TestManager testManager)
        {
            _userManger = testManager;
            _identityContext = identityDbContext;
        }

        public async Task<UserContract> Create(UserContract dto, CancellationToken cancel)
        {
            var user = new IdentityUser();
            await _userManger.CreateAsync(user).ConfigureAwait(false);
            await _identityContext.SaveChangesAsync(cancel).ConfigureAwait(false);

            await UpdateUser(user, dto, cancel).ConfigureAwait(false);

            return await Get(user.Id, cancel).ConfigureAwait(false);
        }
        public async Task<ListDto<UserContract>> Delete(string id, int page, int pageSize, CancellationToken cancel)
        {
            var user = await _userManger.FindByIdAsync(id).ConfigureAwait(false);

            _= await _userManger.DeleteAsync(user).ConfigureAwait(false);
            _ = await _identityContext.SaveChangesAsync(cancel).ConfigureAwait(false);

            return await GetAll(page, pageSize, cancel).ConfigureAwait(false);
        }

        public async Task<UserContract> Get(string id, CancellationToken cancel)
        {
            var user = await _userManger.FindByIdAsync(id).ConfigureAwait(false);
            var dto = user.Adapt<UserContract>();

            dto.UserClaims = await _identityContext.UserClaims
                .Where(x => x.UserId == id)
                .ProjectToType<UserClaimsContract>()
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            dto.UserRoles = await _identityContext.UserRoles
                .Where(x=> x.UserId == id)
                .Select(x=>x.RoleId)
                .ToListAsync(cancel)
                .ConfigureAwait(false);
            return dto;
        }

        public async Task<ListDto<UserContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            var users = await _userManger.Users
                .Skip(page * pageSize)
                .Take(pageSize)
                .ProjectToType<UserContract>()
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            var userIds = users.Select(x => x.Id);

            var userRoles = await _identityContext.UserRoles
                  .Where(x => userIds.Contains(x.UserId))
                  .ToListAsync(cancel)
                  .ConfigureAwait(false);

            var userClaims = await _identityContext.UserClaims
                  .Where(x => userIds.Contains(x.UserId))
                  .ProjectToType<UserClaimsContract>()
                  .ToListAsync(cancel)
                  .ConfigureAwait(false);

            users.ForEach(x =>
            {
                x.UserRoles = userRoles
                    .Where(y => y.UserId == x.Id)
                    .Select(x => x.RoleId)
                    .ToList();

                x.UserClaims = userClaims
                    .Where(y => y.UserId == x.Id)
                    .ToList();
            });

            return new ListDto<UserContract>
            {
                Items = users,
                Page = page,
                PageSize = pageSize,
                TotalItems = users.Count
            };
        }

        public async Task<UserContract> Update(UserContract dto, CancellationToken cancel)
        {
            var user = await _userManger.FindByIdAsync(dto.Id).ConfigureAwait(false);

            await UpdateUser(user, dto, cancel).ConfigureAwait(false);

            return await Get(dto.Id, cancel).ConfigureAwait(false);
        }

        private async Task<bool> UpdateUser(IdentityUser user, UserContract dto, CancellationToken cancel)
        {
            dto.NormalizedUserName = _userManger.NormalizeName(dto.UserName);
            dto.NormalizedEmail = _userManger.NormalizeEmail(dto.Email);
            dto.Adapt(user);

            var updateResult = await _userManger.UpdateAsync(user).ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(dto.ReplacePassword))
            {
                await _userManger.AddPasswordAsync(user, dto.ReplacePassword);
            }

            await UpdateRoles(user, dto, cancel).ConfigureAwait(false);
            await UpdateClaims(user, dto, cancel).ConfigureAwait(false);

            await _identityContext.SaveChangesAsync(cancel).ConfigureAwait(false);

            return updateResult.Succeeded;
        }

        private async Task UpdateClaims(IdentityUser user, UserContract dto, CancellationToken cancel)
        {
            var userClaims = await _identityContext.UserClaims
                .Where(x => x.UserId == dto.Id)
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            var claimsToAdd = dto.UserClaims
                .Where(x => x.Id is null)
                .Select(x => new Claim(x.Type,x.Value))
                .ToList();
            await _userManger.AddClaimsAsync(user, claimsToAdd).ConfigureAwait(false);

            var claimsToRemove = userClaims
                .Where(x => !dto.UserClaims.Any(y => y.Id == x.Id))
                .ToList();
            _identityContext.RemoveRange(claimsToRemove);

            var claimsToUpdate = userClaims
                .Where(x => dto.UserClaims.Any(y => y.Id == x.Id))
                .Select(x =>
                {
                    var update = dto.UserClaims.Find(x => x.Id == x.Id);
                    update.Adapt(x);
                    return x;
                })
                .ToList();
            _identityContext.UserClaims.UpdateRange(claimsToUpdate);
        }

        private async Task UpdateRoles(IdentityUser user, UserContract dto, CancellationToken cancel)
        {
            var userRoles = await _identityContext.UserRoles
                .Where(x => x.UserId == user.Id)
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            var rolesToAdd = dto.UserRoles
                .Where(x => !userRoles.Any(y => y.RoleId == x))
                .Select(x=> new IdentityUserRole<string>
                {
                    RoleId = x,
                    UserId = dto.Id
                })
                .ToList();
            var rolesToDelete = userRoles.Where(x => !dto.UserRoles.Contains(x.RoleId)).ToList();

            await _identityContext.UserRoles.AddRangeAsync(rolesToAdd, cancel).ConfigureAwait(false);
            _identityContext.UserRoles.RemoveRange(rolesToDelete);
        }
    }
}
