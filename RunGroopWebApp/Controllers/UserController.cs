﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.ViewModels;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPhotoService _photoService;

        public UserController(IUserRepository userRepository, UserManager<AppUser> userManager, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _photoService = photoService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    Pace = user.Pace,
                    City = user.City,
                    State = user.State,
                    Mileage = user.Mileage,
                    UserName = user.UserName,
                    ProfileImageUrl = user.ProfileImageUrl ?? "/img/avatar-male-4.jpg",
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }

            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                Pace = user.Pace,
                City = user.City,
                State = user.State,
                Mileage = user.Mileage,
                UserName = user.UserName,
                ProfileImageUrl = user.ProfileImageUrl ?? "/img/avatar-male-4.jpg",
            };
            return View(userDetailViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            var editMV = new EditProfileViewModel()
            {
                City = user.City,
                State = user.State,
                Pace = user.Pace,
                Mileage = user.Mileage,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return View(editMV);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditProfile", editVM);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            if (editVM.Image != null) // only update profile image
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                if (photoResult.Error != null)
                {
                    ModelState.AddModelError("Image", "Failed to upload image");
                    return View("EditProfile", editVM);
                }

                if (!string.IsNullOrEmpty(user.ProfileImageUrl))
                {
                    _ = _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }

                user.ProfileImageUrl = photoResult.Url.ToString();
                editVM.ProfileImageUrl = user.ProfileImageUrl;

                await _userManager.UpdateAsync(user);

                return View(editVM);
            }

            user.City = editVM.City;
            user.State = editVM.State;
            user.Pace = editVM.Pace;
            user.Mileage = editVM.Mileage;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Detail", "User", new { user.Id });
        }
    }
}



// public class UserController : Controller
// {
//     private readonly IUserRepository _userRepository;
//     private readonly UserManager<AppUser> _userManager;
//     private readonly IPhotoService _photoService;

//     public UserController(IUserRepository userRepository, UserManager<AppUser> userManager, IPhotoService photoService)
//     {
//         _userRepository = userRepository;
//         _userManager = userManager;
//         _photoService = photoService;
//     }

//     [HttpGet("users")]
//     public async Task<IActionResult> Index()
//     {
//         try
//         {
//             var users = await _userRepository.GetAllUsers();
//             var result = users.Select(user => new UserViewModel
//             {
//                 Id = user.Id,
//                 Pace = user.Pace,
//                 City = user.City,
//                 State = user.State,
//                 Mileage = user.Mileage,
//                 UserName = user.UserName,
//                 ProfileImageUrl = user.ProfileImageUrl ?? "/img/avatar-male-4.jpg"
//             }).ToList();
            
//             return View(result);
//         }
//         catch (Exception)
//         {
//             TempData["Error"] = "Failed to load users";
//             return View(new List<UserViewModel>());
//         }
//     }

//     [HttpGet]
//     public async Task<IActionResult> Detail(string id)
//     {
//         try
//         {
//             var user = await _userRepository.GetUserById(id);
//             if (user == null)
//             {
//                 TempData["Error"] = "User not found";
//                 return RedirectToAction("Index", "Users");
//             }

//             var userDetailViewModel = new UserDetailViewModel
//             {
//                 Id = user.Id,
//                 Pace = user.Pace,
//                 City = user.City,
//                 State = user.State,
//                 Mileage = user.Mileage,
//                 UserName = user.UserName,
//                 ProfileImageUrl = user.ProfileImageUrl ?? "/img/avatar-male-4.jpg"
//             };
//             return View(userDetailViewModel);
//         }
//         catch (Exception)
//         {
//             TempData["Error"] = "Failed to load user details";
//             return RedirectToAction("Index", "Users");
//         }
//     }

//     [HttpGet]
//     [Authorize]
//     public async Task<IActionResult> EditProfile()
//     {
//         try
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//             {
//                 TempData["Error"] = "User not found";
//                 return RedirectToAction("Index", "Home");
//             }

//             var editMV = new EditProfileViewModel
//             {
//                 City = user.City,
//                 State = user.State,
//                 Pace = user.Pace,
//                 Mileage = user.Mileage,
//                 ProfileImageUrl = user.ProfileImageUrl
//             };
//             return View(editMV);
//         }
//         catch (Exception)
//         {
//             TempData["Error"] = "Failed to load profile";
//             return RedirectToAction("Index", "Home");
//         }
//     }

//     [HttpPost]
//     [Authorize]
//     public async Task<IActionResult> EditProfile(EditProfileViewModel editVM)
//     {
//         try
//         {
//             if (!ModelState.IsValid)
//             {
//                 return View(editVM);
//             }

//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//             {
//                 TempData["Error"] = "User not found";
//                 return RedirectToAction("Index", "Home");
//             }

//             if (editVM.Image != null)
//             {
//                 var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
//                 if (photoResult.Error != null)
//                 {
//                     ModelState.AddModelError("Image", "Failed to upload image");
//                     return View(editVM);
//                 }

//                 if (!string.IsNullOrEmpty(user.ProfileImageUrl))
//                 {
//                     await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
//                 }

//                 user.ProfileImageUrl = photoResult.Url.ToString();
//                 editVM.ProfileImageUrl = user.ProfileImageUrl;
//             }

//             user.City = editVM.City;
//             user.State = editVM.State;
//             user.Pace = editVM.Pace;
//             user.Mileage = editVM.Mileage;

//             var result = await _userManager.UpdateAsync(user);
//             if (!result.Succeeded)
//             {
//                 ModelState.AddModelError("", "Failed to update profile");
//                 return View(editVM);
//             }

//             TempData["Success"] = "Profile updated successfully";
//             return RedirectToAction("Detail", "User", new { user.Id });
//         }
//         catch (Exception)
//         {
//             TempData["Error"] = "An error occurred while updating profile";
//             return View(editVM);
//         }
//     }
// }