using System.Security.Cryptography.X509Certificates;
using System.Net;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using BeerApi.DataContext;
using BeerApi.Models;

namespace Project_BackDev.Repositories
{
    public interface ILocationRepository
    {
        Task<Location> AddLocation(Location location);
        Task DeleteLocation(int locationId);
        Task<Location> GetLocation(int locationId);
        Task<List<Location>> GetLocations();
        Task<Location> UpdateLocation(Location location, int locationId);
    }

    public class LocationRepository : ILocationRepository
    {
        private IBeerContext _context;

        public LocationRepository(IBeerContext context)
        {
            _context = context;
        }

        public async Task<Location> GetLocation(int locationId)
        {
            Location loc = await _context.Locations.Where(l => l.LocationId == locationId).SingleOrDefaultAsync();

            if (loc != null)
            {
                return loc;
            }
            else
            {
                throw new Exception("Location not found.");
            }
        }

        public async Task<List<Location>> GetLocations()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task<Location> AddLocation(Location location)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            return location;
        }

        public async Task<Location> UpdateLocation(Location location, int locationId)
        {
            Location updateLocation = await _context.Locations.Where(l => l.LocationId == locationId).SingleOrDefaultAsync();
            if (updateLocation != null)
            {
                updateLocation.City = location.City;
                updateLocation.HouseNumber = location.HouseNumber;
                updateLocation.Postcode = location.Postcode;
                updateLocation.Street = location.Street;
                await _context.SaveChangesAsync();

                return updateLocation;
            }
            else
            {
                throw new Exception("Location to update not found.");
            }
        }

        public async Task DeleteLocation(int locationId)
        {
            Location deleteLocation = await _context.Locations.Where(l => l.LocationId == locationId).SingleOrDefaultAsync();
            if (deleteLocation != null)
            {
                _context.Locations.Remove(deleteLocation);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Location to delete not found.");
            }
        }
    }
}
