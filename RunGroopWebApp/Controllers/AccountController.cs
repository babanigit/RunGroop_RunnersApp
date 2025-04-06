using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        // private readonly ILocationService _locationService;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ApplicationDbContext context
            //  ,ILocationService locationService
            )
        {
            _context = context;
            // _locationService = locationService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(loginViewModel);
                }

                var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
                if (user == null)
                {
                    // Don't reveal that the user doesn't exist for security reasons
                    TempData["Error"] = "Invalid login attempt";
                    return View(loginViewModel);
                }

                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (!passwordCheck)
                {
                    TempData["Error"] = "Invalid login attempt";
                    return View(loginViewModel);
                }

                var signInResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password,
                    isPersistent: loginViewModel.RememberMe, // Add RememberMe to your LoginViewModel if you want this
                    lockoutOnFailure: true);  // Enable account lockout on failed attempts

                if (signInResult.Succeeded)
                {
                    TempData["Success"] = "Welcome back!";
                    return RedirectToAction("Index", "Race");
                }

                if (signInResult.IsLockedOut)
                {
                    TempData["Error"] = "Account is locked. Please try again later";
                    return View(loginViewModel);
                }

                if (signInResult.RequiresTwoFactor)
                {
                    // Handle 2FA if you implement it
                    return RedirectToAction("LoginWith2fa");
                }

                TempData["Error"] = "Invalid login attempt";
                return View(loginViewModel);
            }
            catch (Exception)
            {
                TempData["Error"] = "An unexpected error occurred during login";
                return View(loginViewModel);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(registerViewModel);
                }

                var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
                if (user != null)
                {
                    TempData["Error"] = "This email address is already in use";
                    return View(registerViewModel);
                }

                var newUser = new AppUser
                {
                    Email = registerViewModel.EmailAddress,
                    UserName = registerViewModel.EmailAddress,
                    EmailConfirmed = true
                };

                var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

                if (!newUserResponse.Succeeded)
                {
                    foreach (var error in newUserResponse.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(registerViewModel);
                }

                var roleResult = await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                if (!roleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(newUser);
                    TempData["Error"] = "Registration failed due to role assignment error";
                    return View(registerViewModel);
                }

                TempData["Success"] = "Registration successful!";
                return RedirectToAction("Index", "Race");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Unexpected error: {ex.Message}";
                return View(registerViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Race");
        }

        [HttpGet]
        [Route("Account/Welcome")]
        public IActionResult Welcome(int page = 0)
        {
            if (page == 0)
            {
                return View();
            }
            return View();

        }

        // [HttpGet]
        // public async Task<IActionResult> GetLocation(string location)
        // {
        //     if (location == null)
        //     {
        //         return Json("Not found");
        //     }
        //     var locationResult = await _locationService.GetLocationSearch(location);
        //     return Json(locationResult);
        // }


    }
}