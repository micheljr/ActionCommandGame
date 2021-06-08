using System;
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

            var roleResult = await AddToRole(registerModel.Admin, user);
            if (roleResult)
            {
                return View(registerModel);
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

        [HttpGet]
        public IActionResult Manage()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditEmail(string returnUrl = null)
        {
            returnUrl ??= Url.Action("Index", "Home");
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var model = new EditEmailModel
            {
                ReturnUrl = returnUrl,
                Email = user.Email
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmail(EditEmailModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            user.Email = model.Email;
            user.UserName = model.Email;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong updating the email address.");
                return View(model);
            }
            
            return RedirectToAction("Manage");
        }

        [HttpGet]
        public IActionResult EditPassword(string returnUrl = null)
        {
            returnUrl ??= Url.Action("Index", "Home");

            var model = new EditPasswordModel()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditPassword(EditPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong updating the password.");
                return View(model);
            }

            return RedirectToAction("Manage");
        }
        
        [HttpGet]
        public async Task<IActionResult> EditAuthorization(string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userIsAdmin = await _userManager.IsInRoleAsync(user, "Administrator");
            
            returnUrl ??= Url.Action("Index", "Home");
            var model = new EditAuthorizationModel
            {
                ReturnUrl = returnUrl,
                WantsAdminRights = userIsAdmin
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAuthorization(EditAuthorizationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var result = await AddToRole(model.WantsAdminRights, user);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong updating the authorization.");
                return View(model);
            }

            return RedirectToAction("Manage");
        }

        private async Task<bool> AddToRole(bool wantsAdminRights, IdentityUser user)
        {
            var isInAdminRole = await _userManager.IsInRoleAsync(user, "Administrator");
            if (wantsAdminRights && !isInAdminRole)
            {
                var removeResult = await _userManager.RemoveFromRoleAsync(user, "Gamer");
                var adminResult = await _userManager.AddToRoleAsync(user, "Administrator");
                
                if (!adminResult.Succeeded && !removeResult.Succeeded)
                {
                    foreach (var error in adminResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    foreach (var error in removeResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return false;
                }

                return true;
            }
            
            var isInGamerRole = await _userManager.IsInRoleAsync(user, "Gamer");
            if (!isInGamerRole)
            {
                var removeResult = await _userManager.RemoveFromRoleAsync(user, "Administrator");
                var gamerResult = await _userManager.AddToRoleAsync(user, "Gamer");
                if (!gamerResult.Succeeded && removeResult.Succeeded) 
                {
                    foreach (var error in gamerResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    foreach (var error in removeResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                
                    return false;
                }
            }

            return true;
        }
    }
}