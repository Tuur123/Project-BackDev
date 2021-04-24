using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using BeerApi.DataContext;
using BeerApi.Models;

namespace BeerApi.Repositories
{
    public interface IBeerRepository
    {
        Task<Beer> AddBeer(Beer beer);
        Task<Beer> GetBeer(int beerId);
        Task<List<Beer>> GetBeers();
    }

    public class BeerRepository : IBeerRepository
    {
        private IBeerContext _context;

        public BeerRepository(IBeerContext context)
        {
            _context = context;
        }

        public async Task<List<Beer>> GetBeers()
        {
            return await _context.Beers.ToListAsync();
        }

        public async Task<Beer> GetBeer(int beerId)
        {
            return await _context.Beers.Where(b => b.BeerId == beerId).SingleOrDefaultAsync();
        }

        public async Task<Beer> AddBeer(Beer beer)
        {
            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();
            return beer;
        }
    }
}
