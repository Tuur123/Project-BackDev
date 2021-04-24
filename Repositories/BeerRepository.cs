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
        Task<Beer> DeleteBeer(int beerId);
        Task<Beer> GetBeer(int beerId);
        Task<List<Beer>> GetBeers();
        Task<Beer> UpdateBeer(Beer beer, int beerId);
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

        public async Task<Beer> UpdateBeer(Beer beer, int beerId)
        {
            Beer updateBeer = await _context.Beers.Where(b => b.BeerId == beerId).SingleOrDefaultAsync();
            if (updateBeer != null)
            {
                updateBeer.AlchoholPercentage = beer.AlchoholPercentage;
                updateBeer.Brewer = beer.Brewer;
                updateBeer.Name = beer.Name;

                await _context.SaveChangesAsync();
                return updateBeer;
            }
            else
            {
                throw new Exception("Beer to update not found.");
            }
        }

        public async Task<Beer> DeleteBeer(int beerId)
        {
            Beer deleteBeer = await _context.Beers.Where(b => b.BeerId == beerId).SingleOrDefaultAsync();

            if (deleteBeer != null)
            {
                _context.Beers.Remove(deleteBeer);
                await _context.SaveChangesAsync();
                return deleteBeer;
            }
            else
            {
                throw new Exception("Beer to delete not found.");
            }
        }
    }
}
