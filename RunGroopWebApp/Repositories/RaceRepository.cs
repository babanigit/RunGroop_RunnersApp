using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunGroopWebApp.Data;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Repositories
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;
        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public bool Add(Race race)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Race race)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Race>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Race>> GetAllRacesByCity(string city)
        {
            throw new NotImplementedException();
        }

        public Task<Race?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Race?> GetByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountByCategoryAsync(RaceCategory category)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Race>> GetRacesByCategoryAndSliceAsync(RaceCategory category, int offset, int size)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Race>> GetSliceAsync(int offset, int size)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Race race)
        {
            throw new NotImplementedException();
        }
    }
}