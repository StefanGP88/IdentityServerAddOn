using Ids.SimpleAdmin.Backend.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace RazorTestLibrary.Areas.SimpleAdmin.Pages.Roles
{
    public class AllModel : PageModel
    {
        [BindProperty]
        public ListDto<RoleResponseDto> Roles { get; set; }
        public void OnGet()
        {
            Roles = new ListDto<RoleResponseDto>()
            {
                Page = 1,
                PageSize = 5,
                Total = 23,
                Items = new System.Collections.Generic.List<RoleResponseDto>()
            };
            for (int i = 0; i < 5; i++)
            {
                Roles.Items.Add(new RoleResponseDto
                {
                    Id = ((Roles.Page * Roles.PageSize) + i).ToString() + "SOMEID23234234",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = "ROLE" + i,
                    RoleName = "Role" + i
                });
            }
        }
    }
}
