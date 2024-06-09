using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;

namespace MVC_Mini_Project.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<SelectList> GetAllSelectedAvailableAsync(AppUser user)
        {
            var roles = _roleManager.Roles.ToList();

            List<IdentityRole> availableRoles = new();

            foreach (var role in roles)
            {
                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    availableRoles.Add(role);
                }
            }

            return new SelectList(availableRoles, "Id", "Name");
        }
    }
}
