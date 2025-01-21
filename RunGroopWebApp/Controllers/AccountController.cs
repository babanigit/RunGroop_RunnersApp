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

        public AccountController(UserManager<AppUser> userManager,
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
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            Console.WriteLine($"hehehe- after the findbyemailasync");

            if (user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    Console.WriteLine($"hehehe- after the passwordcheck");

                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        Console.WriteLine($"hehehe- after the success");

                        return RedirectToAction("Index", "Race");
                    }
                }
                Console.WriteLine($"hehehe- after the password incorrect");

                //Password is incorrect
                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginViewModel);
            }
            Console.WriteLine($"hehehe- after the user not found");

            //User not found
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginViewModel);
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