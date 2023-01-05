using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopDemoApp.Core.Constants;
using WebShopDemoApp.Core.Data.Models.Account;
using WebShopDemoApp.Models;

namespace WebShopDemoApp.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(
            UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        //Register 
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
            => View(new RegisterViewModel());

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            // Claim
            await userManager
                .AddClaimAsync(user, new System.Security.Claims.Claim(ClaimTypeConstants.FirstName, user.FirstName ?? user.Email));

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach(var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        //Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {   

                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    if (model.ReturnUrl != null)
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login");

            return View(model);
        }

        //Logout
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        //Role
        public async Task<IActionResult> CreateRoles()
        {
            await roleManager.CreateAsync(new IdentityRole(RoleConstants.Manager));
            await roleManager.CreateAsync(new IdentityRole(RoleConstants.Supervisor));
            await roleManager.CreateAsync(new IdentityRole(RoleConstants.Administrator));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddUsersToRoles()
        {
            string email1 = "test123@abv.bg";
            string email2 = "pesho@abv.bg";

            var user = await userManager.FindByEmailAsync(email1);
            var user2 = await userManager.FindByEmailAsync(email2);

            await userManager.AddToRoleAsync(user, RoleConstants.Manager);
            await userManager.AddToRolesAsync(user2, new string[] { RoleConstants.Supervisor, RoleConstants.Manager });
            //await userManager.Users.ToListAsync(); - Get all users

            return RedirectToAction("Index", "Home");
        }
    }
}
