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
        Task<Business> AddBusiness(Business business);
        Task DeleteBusiness(int businessId);
        Task<Business> GetBusiness(int businessId);
        Task<List<Business>> GetBusinesses();
        Task<Business> UpdateBusiness(Business business, int businessId);
    }

    public class BusinessRepository : IBusinessRepository
    {
        private IBeerContext _context;

        public BusinessRepository(IBeerContext context)
        {
            _context = context;
        }

        public async Task<Business> GetBusiness(int businessId)
        {
            Business business = await _context.Businesses.Where(b => b.BusinessId == businessId).SingleOrDefaultAsync();

            if (business != null)
            {
                return business;
            }
            else
            {
                throw new Exception("Business not found.");
            }
        }

        public async Task<List<Business>> GetBusinesses()
        {
            return await _context.Businesses.ToListAsync();
        }

        public async Task<Business> AddBusiness(Business business)
        {
            await _context.Businesses.AddAsync(business);
            await _context.SaveChangesAsync();
            return business;
        }

        public async Task<Business> UpdateBusiness(Business business, int businessId)
        {
            Business updateBusiness = await _context.Businesses.Where(b => b.BusinessId == businessId).SingleOrDefaultAsync();
            if (updateBusiness != null)
            {
                updateBusiness.Email = business.Email;
                updateBusiness.LocationId = business.LocationId;
                updateBusiness.Name = business.Name;
                updateBusiness.Type = business.Type;
                updateBusiness.BusinessBeers = business.BusinessBeers;
                await _context.SaveChangesAsync();
                return updateBusiness;
            }
            else
            {
                throw new Exception("Business to update not found.");
            }
        }

        public async Task DeleteBusiness(int businessId)
        {
            Business deleteBusiness = await _context.Businesses.Where(b => b.BusinessId == businessId).SingleOrDefaultAsync();

            if (deleteBusiness != null)
            {
                _context.Businesses.Remove(deleteBusiness);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Business to delete not found.");
            }
        }
    }
}
