using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
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
            userResult.CheckResult("CreateUserError");

            var roles = dto.UserRoles.ConvertAll(x => x.ToString());
            var roleResult = await _userManger.AddToRolesAsync(user, roles).ConfigureAwait(false);
            roleResult.CheckResult("AddRolesError");

            var userClaims = dto.UserClaims.ConvertAll(x => new Claim(x.ClaimType, x.ClaimValue));
            var claimResult = await _userManger.AddClaimsAsync(user, userClaims).ConfigureAwait(false);
            claimResult.CheckResult("AddClaimsError");

            return await Get(user.Id, cancel).ConfigureAwait(false);
        }
        public async Task<ListDto<UserContract>> Delete(string id, int page, int pageSize, CancellationToken cancel)
        {
            var user = await _userManger.FindByIdAsync(id).ConfigureAwait(false);
            if (user is null) throw new Exception("User not found");

            var deleteResult = await _userManger.DeleteAsync(user).ConfigureAwait(false);
            deleteResult.CheckResult("DeleteUserError");

            return await GetAll(page, pageSize, cancel).ConfigureAwait(false);
        }

        public async Task<UserContract> Get(string id, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public async Task<ListDto<UserContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }

        public async Task<UserContract> Update(UserContract dto, CancellationToken cancel)
        {
            throw new NotImplementedException();
        }
    }
}
