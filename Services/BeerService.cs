using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BeerApi.DTO;
using BeerApi.Models;
using BeerApi.Repositories;

namespace BeerApi.Services
{
    public interface IBeerService
    {
        // Beers
        Task AddBeer(BeerDTO beer);
        Task DeleteBeer(Guid beerId);
        Task<BeerDTO> GetBeer(Guid beerId);
        Task<List<BeerDTO>> GetBeers();
        Task UpdateBeer(BeerUpdateDTO beer);

        // Locations
        Task AddLocation(LocationDTO location);
        Task DeleteLocation(Guid locationId);
        Task<LocationDTO> GetLocation(Guid locationId);
        Task<List<LocationDTO>> GetLocations();
        Task UpdateLocation(LocationUpdateDTO location);

        // Businesses
        Task AddBusiness(BusinessDTO business);
        Task DeleteBusiness(Guid businessId);
        Task<BusinessDTO> GetBusiness(Guid businessId);
        Task<List<BusinessDTO>> GetBusinesses();
        Task UpdateBusiness(BusinessDTO business);
    }
    public class BeerService : IBeerService
    {
        private IBeerRepository _beerRepository;
        private ILocationRepository _locationRepository;
        private IBusinessRepository _businessRepository;
        private IMapper _mapper;

        public BeerService(IMapper mapper, IBeerRepository beerRepository, IBusinessRepository businessRepository, ILocationRepository locationRepository)
        {
            _beerRepository = beerRepository;
            _locationRepository = locationRepository;
            _businessRepository = businessRepository;
            _mapper = mapper;
        }

        #region Beers
        public async Task<BeerDTO> GetBeer(Guid beerId)
        {
            try
            {
                return _mapper.Map<BeerDTO>(await _beerRepository.GetBeer(beerId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BeerDTO>> GetBeers()
        {
            try
            {
                return _mapper.Map<List<BeerDTO>>(await _beerRepository.GetBeers());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddBeer(BeerDTO beer)
        {
            try
            {
                Beer newBeer = _mapper.Map<Beer>(beer);

                newBeer.BusinessBeers = new List<BusinessBeer>();
                newBeer.BeerId = Guid.NewGuid();

                foreach (Guid businessId in beer.Businesses)
                {
                    newBeer.BusinessBeers.Add(new BusinessBeer() { BusinessId = businessId, BeerId = newBeer.BeerId });
                }

                await _beerRepository.AddBeer(newBeer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateBeer(BeerUpdateDTO beer)
        {
            Beer updateBeer = _mapper.Map<Beer>(beer);

            try
            {
                await _beerRepository.UpdateBeer(updateBeer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteBeer(Guid beerId)
        {
            try
            {
                await _beerRepository.DeleteBeer(beerId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion Beers

        #region Locations
        public async Task<LocationDTO> GetLocation(Guid locationId)
        {
            try
            {
                return _mapper.Map<LocationDTO>(await _locationRepository.GetLocation(locationId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<LocationDTO>> GetLocations()
        {
            try
            {
                return _mapper.Map<List<LocationDTO>>(await _locationRepository.GetLocations());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddLocation(LocationDTO location)
        {
            try
            {
                Location newLocation = _mapper.Map<Location>(location);
                await _locationRepository.AddLocation(newLocation);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateLocation(LocationUpdateDTO location)
        {
            Location updateLocation = _mapper.Map<Location>(location);

            try
            {
                await _locationRepository.UpdateLocation(updateLocation);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteLocation(Guid locationId)
        {
            try
            {
                await _locationRepository.DeleteLocation(locationId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion Locations

        #region Businesses
        public async Task<BusinessDTO> GetBusiness(Guid businessId)
        {
            try
            {
                return _mapper.Map<BusinessDTO>(await _businessRepository.GetBusiness(businessId));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BusinessDTO>> GetBusinesses()
        {
            try
            {
                return _mapper.Map<List<BusinessDTO>>(await _businessRepository.GetBusinesses());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddBusiness(BusinessDTO business)
        {
            try
            {
                Business newBusiness = _mapper.Map<Business>(business);

                newBusiness.BusinessBeers = new List<BusinessBeer>();
                newBusiness.BusinessId = Guid.NewGuid();

                foreach (Guid beerId in business.Beers)
                {
                    newBusiness.BusinessBeers.Add(new BusinessBeer() { BeerId = beerId, BusinessId = newBusiness.BusinessId });
                }

                await _businessRepository.AddBusiness(newBusiness);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateBusiness(BusinessDTO business)
        {

            Business updateBusiness = _mapper.Map<Business>(business);

            try
            {
                await _businessRepository.UpdateBusiness(updateBusiness);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteBusiness(Guid businessId)
        {
            try
            {
                await _businessRepository.DeleteBusiness(businessId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion Businesses
    }
}