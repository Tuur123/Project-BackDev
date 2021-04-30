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
        Task AddBeer(Beer beer);
        Task DeleteBeer(Guid beerId);
        Task<Beer> GetBeer(Guid beerId);
        Task<List<Beer>> GetBeers();
        Task UpdateBeer(Beer beer);
    }

    public class BeerRepository : IBeerRepository
    {
        private IBeerContext _context;

        public BeerRepository(IBeerContext context)
        {
            _context = context;
        }

        public async Task<Beer> GetBeer(Guid beerId)
        {
            return await _context.Beers.Where(b => b.BeerId == beerId).Include(b => b.BusinessBeers).SingleOrDefaultAsync();
        }

        public async Task<List<Beer>> GetBeers()
        {
            return await _context.Beers.Include(b => b.BusinessBeers).ToListAsync();
        }

        public async Task AddBeer(Beer beer)
        {
            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBeer(Beer beer)
        {
            Beer beerToUpdate = await _context.Beers.Where(b => b.BeerId == beer.BeerId).SingleOrDefaultAsync();

            beerToUpdate.AlchoholPercentage = beer.AlchoholPercentage;
            beerToUpdate.Brewer = beer.Brewer;
            beerToUpdate.Name = beer.Name;
            beerToUpdate.BusinessBeers = beer.BusinessBeers;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBeer(Guid beerId)
        {
            Beer deleteBeer = await _context.Beers.Where(b => b.BeerId == beerId).SingleOrDefaultAsync();
            _context.Beers.Remove(deleteBeer);
            await _context.SaveChangesAsync();
        }
    }
}
