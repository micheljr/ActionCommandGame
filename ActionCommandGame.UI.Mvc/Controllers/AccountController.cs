using System.Threading.Tasks;
using ActionCommandGame.UI.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ActionCommandGame.UI.Mvc.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            var registerModel = new RegisterModel { ReturnUrl = returnUrl };
            return View(registerModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }

            var user = new IdentityUser { UserName = registerModel.Email, Email = registerModel.Email };
            var userResult = await _userManager.CreateAsync(user, registerModel.Password);

            if (!userResult.Succeeded)
            {
                foreach (var error in userResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(registerModel);
            }

            if (registerModel.Admin)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "Administrator");
                
                if (!roleResult.Succeeded)
                {
                    foreach (var error in userResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(registerModel);
                }
            }
            else
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "Gamer"); 
                
                if (!roleResult.Succeeded)
                {
                    foreach (var error in userResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(registerModel);
                }
            }

            await _signInManager.SignInAsync(user, false);

            registerModel.ReturnUrl ??= Url.Action("Index", "Home");
            return LocalRedirect(registerModel.ReturnUrl);
        }
        
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            returnUrl ??= Url.Action("Index", "Home");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var loginModel = new LoginModel{ReturnUrl = returnUrl};
            return View(loginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password,
                loginModel.RememberMe, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginModel);
            }

            loginModel.ReturnUrl ??= Url.Action("Index", "Home");
            return LocalRedirect(loginModel.ReturnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            returnUrl ??= Url.Action("Index", "Home");
            return LocalRedirect(returnUrl);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}