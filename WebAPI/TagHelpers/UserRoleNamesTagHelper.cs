
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace WebAPI.TagHelpers
{
    public class UserRoleNamesTagHelper:TagHelper
    {
        public string UserId { get; set; } = null!;

        private readonly UserManager<ApplicationUser> _userManager;

        public UserRoleNamesTagHelper(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var userRoles = await _userManager.GetRolesAsync(user!);

            var stringBuilder = new StringBuilder();

            userRoles.ToList().ForEach(role =>
            {
                stringBuilder.Append(@$"<span class='badge bg-secondary mx-1'>{role.ToLower()}</span>");
            });

            output.Content.SetHtmlContent(stringBuilder.ToString());
        }
    }
}
