using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace RazorTestLibrary.Areas.SimpleAdmin.Pages.Users
{
    public class AllModel : BasePageModel<UserResponseDto>
    {
        private readonly IUserHandler _handler;
        public AllModel(IUserHandler handler)
        {
            _handler = handler;
        }
        public IActionResult OnGet(CancellationToken cancel = default)
        {
            List = _handler.ReadAllUsers(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }
        public IActionResult OnPostAdd([FromForm] CreateUserRequestDto dto, CancellationToken cancel = default)
        {
            _handler.CreateUser(dto).GetAwaiter().GetResult();
            List = _handler.ReadAllUsers(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }

        public IActionResult OnPostEdit([FromForm] UpdateUserRequestDto dto, CancellationToken cancel = default)
        {
            _handler.UpdateUser(dto).GetAwaiter().GetResult();
            List = _handler.ReadAllUsers(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }

        public IActionResult OnPostDelete(string userId,  CancellationToken cancel = default)
        {
            _handler.DeleteUser(userId).GetAwaiter().GetResult();
            List = _handler.ReadAllUsers(PageNumber, PageSize, cancel).GetAwaiter().GetResult();
            return Page();
        }
    }
}
