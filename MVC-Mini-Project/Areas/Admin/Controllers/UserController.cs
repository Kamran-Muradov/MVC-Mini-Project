using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Users;

namespace MVC_Mini_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public UserController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IRoleService roleService,
            IUserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleService = roleService;
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            var usersWithRoles = await _userService.GetAllWithRolesMappedAsync(users);

            return View(usersWithRoles);
        }

        [HttpGet]
        public async Task<IActionResult> AddRole(string id)
        {
            if (id is null) return BadRequest();

            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return NotFound();

            ViewBag.roles = await _roleService.GetAllSelectedAvailableAsync(user);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(string id, UserAddRoleVM request)
        {
            if (id is null) return BadRequest();

            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return NotFound();

            var role = await _roleManager.FindByIdAsync(request.RoleId);

            await _userManager.AddToRoleAsync(user, role.Name);

            return RedirectToAction(nameof(Index));
        }
    }
}
