using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly SignInManager<UserViewModel> _signInManager;
        private readonly UserManager<UserViewModel> _userManager;

        public AccountsController(UserManager<UserViewModel> userManager,
            SignInManager<UserViewModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Register(model);
            }

            return RegisterInternal(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Login(model);
            }

            return LoginInternal(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Edit(model);
            }

            return EditInternal(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        private async Task<IActionResult> RegisterInternal(RegisterViewModel model)
        {
            var user = new UserViewModel
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                Surname = model.Surname,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return RedirectToAction(nameof(Login));
        }

        private async Task<IActionResult> LoginInternal(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("ShowDrivers", "Drivers");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login/password");

                return Login();
            }
        }

        private async Task<IActionResult> EditInternal(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            user.FirstName = model.FirstName;
            user.Surname = model.Surname;
            user.Patronic = model.Patronic;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Failed to update data");
            }

            return View();
        }
    }
}