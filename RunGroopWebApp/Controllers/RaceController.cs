using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.ViewModels;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IRaceRepository _raceRepository;

        public RaceController(IRaceRepository raceRepository, IPhotoService photoService)
        {
            _raceRepository = raceRepository;
            _photoService = photoService;

        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> clubs = await _raceRepository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> DetailRace(int id)
        {
            Race clubs = await _raceRepository.GetByIdAsync(id);
            return View(clubs);
        }



        public IActionResult Create()
        {
            // var curUserId = HttpContext.User.GetUserId();
            // var createClubViewModel = new CreateClubViewModel { AppUserId = curUserId };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
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
                var result = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    // AppUserId = raceVM.AppUserId,
                    RaceCategory = raceVM.RaceCategory,
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                    }
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(raceVM);
        }
    }
}