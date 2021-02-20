using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Ids.SimpleAdmin.Frontend.Areas.SimpleAdmin.Pages.Users
{
    public class AllModel : BasePageModel<UserResponseDto, UserProperties>
    {
        private readonly IUserHandler _userHandler;
        private readonly IRoleHandler _roleHandler;
        public ListDto<RoleResponseDto> UserRoleList { get; set; }

        public AllModel(IUserHandler userHandler, IRoleHandler roleHandler)
        {
            _userHandler = userHandler;
            _roleHandler = roleHandler;
        }
        public IActionResult OnGet(CancellationToken cancel = default)
        {
            List = _userHandler.ReadAllUsers(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            UserRoleList = _roleHandler.ReadAllRoles(cancel).GetAwaiter().GetResult();
            return Page();
        }
        public IActionResult OnPostAdd([FromForm] CreateUserRequestDto dto, CancellationToken cancel = default)
        {
            _userHandler.CreateUser(dto).GetAwaiter().GetResult();
            List = _userHandler.ReadAllUsers(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            UserRoleList = _roleHandler.ReadAllRoles(cancel).GetAwaiter().GetResult();
            return Page();
        }

        public IActionResult OnPostEdit([FromForm] UpdateUserRequestDto dto, CancellationToken cancel = default)
        {
            _userHandler.UpdateUser(dto).GetAwaiter().GetResult();
            List = _userHandler.ReadAllUsers(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            UserRoleList = _roleHandler.ReadAllRoles(cancel).GetAwaiter().GetResult();
            return Page();
        }

        public IActionResult OnPostDelete(string userId,  CancellationToken cancel = default)
        {
            _userHandler.DeleteUser(userId).GetAwaiter().GetResult();
            List = _userHandler.ReadAllUsers(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            UserRoleList = _roleHandler.ReadAllRoles(cancel).GetAwaiter().GetResult();
            return Page();
        }

    }
        public class UserProperties
        {

        }
}
