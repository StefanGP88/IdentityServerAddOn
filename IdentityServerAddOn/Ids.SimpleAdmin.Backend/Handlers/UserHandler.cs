using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        private readonly UserManager<IdentityUser> _userManger;

        private readonly IdentityDbContext _identityContext;
        public UserHandler(UserManager<IdentityUser> userManger)
        {
            _userManger = userManger;
        }

        public async Task<UserContract> Create(UserContract dto, CancellationToken cancel)
        {
            dto.NormalizedUserName = _userManger.NormalizeName(dto.UserName);
            dto.NormalizedEmail = _userManger.NormalizeEmail(dto.Email);

            var user = dto.Adapt<IdentityUser>();

            var userResult = await _userManger.CreateAsync(user, dto.ReplacePassword).ConfigureAwait(false);
            var createErrors = userResult.CheckResult("CreateUserError");
            if (createErrors is not null) throw createErrors;

            var roles = dto.UserRoles.ConvertAll(x => x.ToString());
            var roleResult = await _userManger.AddToRolesAsync(user, roles).ConfigureAwait(false);
            var roleErrors = roleResult.CheckResult("AddRolesError");
            await DeleteUserIfErrorThenThrow(roleErrors, user.Id, cancel).ConfigureAwait(false);

            var userClaims = dto.UserClaims.ConvertAll(x => new Claim(x.Type, x.Value));
            var claimResult = await _userManger.AddClaimsAsync(user, userClaims).ConfigureAwait(false);
            var claimError = claimResult.CheckResult("AddClaimsError");
            await DeleteUserIfErrorThenThrow(claimError, user.Id, cancel).ConfigureAwait(false);

            return await Get(user.Id, cancel).ConfigureAwait(false);
        }
        public async Task<ListDto<UserContract>> Delete(string id, int page, int pageSize, CancellationToken cancel)
        {
            var user = await _userManger.FindByIdAsync(id).ConfigureAwait(false);
            CheckIfFound(user);

            var deleteResult = await _userManger.DeleteAsync(user).ConfigureAwait(false);
            deleteResult.CheckResult("DeleteUserError");

            return await GetAll(page, pageSize, cancel).ConfigureAwait(false);
        }

        public async Task<UserContract> Get(string id, CancellationToken cancel)
        {
            var user = await _userManger.FindByIdAsync(id).ConfigureAwait(false);
            CheckIfFound(user);
            var dto = user.Adapt<UserContract>();

            var claims = await _userManger.GetClaimsAsync(user).ConfigureAwait(false);
            var userClaims = claims.ToList();
            dto.UserClaims = userClaims.ConvertAll(x =>
            {
                return new UserClaimsContract
                {
                    UserId = id,
                    Type = x.Type,
                    Value = x.Value
                };
            });

            dto.UserRoles = (List<Guid>)await _userManger.GetRolesAsync(user).ConfigureAwait(false);
            return dto;
        }

        public async Task<ListDto<UserContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public async Task<UserContract> Update(UserContract dto, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }
        private static void CheckIfFound(IdentityUser user)
        {
            if (user is null) throw new Exception("User not found");
        }
        private async Task DeleteUserIfErrorThenThrow(AggregateException aggregateException, string userId, CancellationToken cancel)
        {
            if (aggregateException is not null)
            {
                _ = await Delete(userId, 0, 0, cancel).ConfigureAwait(false);
                throw aggregateException;
            }
        }
    }
}
