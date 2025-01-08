using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> DetailClub(int id)
        {
            Club clubs = await _clubRepository.GetByIdAsync(id);
            return View(clubs);
        }

        public IActionResult Create()
        {
            // var curUserId = HttpContext.User.GetUserId();
            // var createClubViewModel = new CreateClubViewModel { AppUserId = curUserId };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {

//getting error here in ModelState, Error: The AppUserId field is required. 
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"Key: {error.Key}");
                    foreach (var subError in error.Value.Errors)
                    {
                        Console.WriteLine($"Error: {subError.ErrorMessage}");
                    }
                }
            }
            if (ModelState.IsValid)
            {
                Console.WriteLine($"this is before result");

                var result = await _photoService.AddPhotoAsync(clubVM.Image);
                Console.WriteLine($"this is result: {result}");

                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    ClubCategory = clubVM.ClubCategory,
                    AppUserId = clubVM.AppUserId,
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                    }
                };

                Console.WriteLine($"this is club: {club}");

                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                Console.WriteLine($"this is photo upload fail");

                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(clubVM);
        }

    }
}