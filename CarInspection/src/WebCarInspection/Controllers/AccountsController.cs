using DataAccessLayer.DTO.DB;
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
        private readonly SignInManager<UserDto> _signInManager;
        private readonly UserManager<UserDto> _userManager;

        public AccountsController(UserManager<UserDto> userManager,
            SignInManager<UserDto> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult UserRegistration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public Task<IActionResult> UserRegistration(UserRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return UserRegistration(model);
            }

            return UserRegistrationInternal(model);
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
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> EditProfile(UserDto model)
        {
            if (!ModelState.IsValid)
            {
                return EditProfile(model);
            }

            return EditProfileInternal(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        private async Task<IActionResult> UserRegistrationInternal(UserRegistrationViewModel model)
        {
            var user = new UserDto
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

        private async Task<IActionResult> EditProfileInternal(UserDto model)
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