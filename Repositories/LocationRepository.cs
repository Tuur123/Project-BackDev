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
        Task DeleteLocation(Guid locationId);
        Task<Location> GetLocation(Guid locationId);
        Task<List<Location>> GetLocations();
        Task UpdateLocation(Location location);
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

        public async Task UpdateLocation(Location location)
        {
            Location updateLocation = await _context.Locations.Where(l => l.LocationId == location.LocationId).SingleOrDefaultAsync();

            updateLocation.City = location.City;
            updateLocation.HouseNumber = location.HouseNumber;
            updateLocation.Postcode = location.Postcode;
            updateLocation.Street = location.Street;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteLocation(Guid locationId)
        {
            Location deleteLocation = await _context.Locations.Where(l => l.LocationId == locationId).SingleOrDefaultAsync();
            _context.Locations.Remove(deleteLocation);
            await _context.SaveChangesAsync();
        }
    }
}
