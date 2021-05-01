using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using BeerApi.DataContext;
using BeerApi.Models;

namespace BeerApi.Repositories
{
    public interface IBusinessRepository
    {
        Task AddBusiness(Business business);
        Task DeleteBusiness(Guid businessId);
        Task<Business> GetBusiness(Guid businessId);
        Task<List<Business>> GetBusinesses();
        Task<Business> UpdateBusiness(Business business);
    }

    public class BusinessRepository : IBusinessRepository
    {
        private IBeerContext _context;

        public BusinessRepository(IBeerContext context)
        {
            _context = context;
        }

        public async Task<Business> GetBusiness(Guid businessId)
        {
            Business business = await _context.Businesses
            .Include(b => b.BusinessBeers)
            .Include(b => b.Location)
            .Where(b => b.BusinessId == businessId)
            .SingleOrDefaultAsync();

            return business;
        }

        public async Task<List<Business>> GetBusinesses()
        {
            return await _context.Businesses.Include(b => b.BusinessBeers).Include(b => b.Location).ToListAsync();
        }

        public async Task AddBusiness(Business business)
        {
            await _context.Businesses.AddAsync(business);
            await _context.SaveChangesAsync();
        }

        public async Task<Business> UpdateBusiness(Business business)
        {

            Business updateBusiness = await _context.Businesses.Where(b => b.BusinessId == business.BusinessId).SingleOrDefaultAsync();

            if (updateBusiness == null)
            {
                return null;
            }

            // oude relaties verwijderen
            _context.BusinessBeers.RemoveRange(await _context.BusinessBeers.Where(b => b.BusinessId == updateBusiness.BusinessId).ToListAsync());

            // business updaten
            updateBusiness = business;

            // nieuwe relaties toevoegen
            foreach (BusinessBeer businessBeer in updateBusiness.BusinessBeers)
            {
                await _context.BusinessBeers.AddAsync(businessBeer);
            }

            await _context.SaveChangesAsync();

            return await GetBusiness(updateBusiness.BusinessId);
        }

        public async Task DeleteBusiness(Guid businessId)
        {
            Business deleteBusiness = await _context.Businesses.Where(b => b.BusinessId == businessId).SingleOrDefaultAsync();

            _context.Businesses.Remove(deleteBusiness);
            await _context.SaveChangesAsync();
        }
    }
}
