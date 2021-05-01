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
        Task<Beer> DeleteBeer(Guid beerId);
        Task<Beer> GetBeer(Guid beerId);
        Task<List<Beer>> GetBeers();
        Task<Beer> UpdateBeer(Beer beer);
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

        public async Task<Beer> AddBeer(Beer beer)
        {
            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();

            return beer;
        }

        public async Task<Beer> UpdateBeer(Beer beer)
        {
            Beer beerToUpdate = await _context.Beers.Where(b => b.BeerId == beer.BeerId).SingleOrDefaultAsync();

            if (beerToUpdate == null)
            {
                return null;
            }

            beerToUpdate.AlchoholPercentage = beer.AlchoholPercentage;
            beerToUpdate.Brewer = beer.Brewer;
            beerToUpdate.Name = beer.Name;
            beerToUpdate.BusinessBeers = beer.BusinessBeers;

            await _context.SaveChangesAsync();

            return beerToUpdate;
        }

        public async Task<Beer> DeleteBeer(Guid beerId)
        {
            Beer deleteBeer = await _context.Beers.Where(b => b.BeerId == beerId).SingleOrDefaultAsync();

            if (deleteBeer == null)
            {
                return null;
            }

            _context.Beers.Remove(deleteBeer);
            await _context.SaveChangesAsync();

            return deleteBeer;
        }
    }
}
