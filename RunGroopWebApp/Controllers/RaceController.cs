using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        public RaceController(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
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
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
            {
                return View(race);
            }
            _raceRepository.Add(race);
            return RedirectToAction("Index");
        }
    }
}