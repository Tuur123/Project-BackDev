using System.Security.Cryptography.X509Certificates;
using System.Net;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using BeerApi.DataContext;
using BeerApi.Models;

namespace BeerApi.Repositories
{
    public interface ILocationRepository
    {
        Task AddLocation(Location location);
        Task<Location> DeleteLocation(Guid locationId);
        Task<Location> GetLocation(Guid locationId);
        Task<List<Location>> GetLocations();
        Task<Location> UpdateLocation(Location location);
    }

    public class LocationRepository : ILocationRepository
    {
        private IBeerContext _context;

        public LocationRepository(IBeerContext context)
        {
            _context = context;
        }

        public async Task<Location> GetLocation(Guid locationId)
        {
            return await _context.Locations.Where(l => l.LocationId == locationId).SingleOrDefaultAsync();
        }

        public async Task<List<Location>> GetLocations()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task AddLocation(Location location)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
        }

        public async Task<Location> UpdateLocation(Location location)
        {
            Location updateLocation = await _context.Locations.Where(l => l.LocationId == location.LocationId).SingleOrDefaultAsync();

            if (updateLocation == null)
            {
                return null;
            }

            updateLocation.City = location.City;
            updateLocation.Postcode = location.Postcode;

            await _context.SaveChangesAsync();

            return updateLocation;
        }

        public async Task<Location> DeleteLocation(Guid locationId)
        {
            Location deleteLocation = await _context.Locations.Where(l => l.LocationId == locationId).SingleOrDefaultAsync();

            if (deleteLocation == null)
            {
                return null;
            }

            _context.Locations.Remove(deleteLocation);
            await _context.SaveChangesAsync();

            return deleteLocation;
        }
    }
}
