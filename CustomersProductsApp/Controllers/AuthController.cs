using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TraineesApp.Models;
using TraineesApp.Viewmodels;

namespace TraineesApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [Route("register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser();
                appUser.Email = model.Email;
                appUser.FirstName = model.FirstName;
                appUser.LastName = model.LastName;
                appUser.Gender = model.Gender;
                appUser.UserName = model.Username;
                appUser.PasswordHash = model.Password;
                IdentityResult result = await userManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
                {
                    var roleExists = await roleManager.RoleExistsAsync("Member");

                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole("Member"));
                    }
                    await userManager.AddToRoleAsync(appUser, "Member");

                    await signInManager.SignInAsync(appUser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("Element", item.Description);
                    }
                }
            }
            return View(model);
        }

        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {

                AppUser appUser = await userManager.FindByNameAsync(model.Username);

                if (appUser != null)
                {
                    bool exist = await userManager.CheckPasswordAsync(appUser, model.Password);

                    if (exist == true)
                    {
                        await signInManager.SignInAsync(appUser, model.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Wrong UserName Or Password!!");
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }


    }


}
