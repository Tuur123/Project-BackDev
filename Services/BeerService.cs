using System.Linq;
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
        Task<BeerDTO> UpdateBeer(BeerDTO beer);

        // Businesses
        Task AddBusiness(AddBusinessDTO business);
        Task DeleteBusiness(Guid businessId);
        Task<BusinessDTO> GetBusiness(Guid businessId);
        Task<List<BusinessDTO>> GetBusinesses();
        Task<BusinessDTO> UpdateBusiness(UpdateBusinessDTO business);

        // Locations
        Task AddLocation(LocationDTO location);
        Task DeleteLocation(Guid locationId);
        Task<LocationDTO> GetLocation(Guid locationId);
        Task<List<LocationDTO>> GetLocations();
        Task<LocationDTO> UpdateLocation(Location location);
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
                await _beerRepository.AddBeer(newBeer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BeerDTO> UpdateBeer(BeerDTO beer)
        {
            try
            {
                Beer updateBeer = _mapper.Map<Beer>(beer);
                return _mapper.Map<BeerDTO>(await _beerRepository.UpdateBeer(updateBeer));
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

        #region Businesses
        public async Task<BusinessDTO> GetBusiness(Guid businessId)
        {
            try
            {
                Business business = await _businessRepository.GetBusiness(businessId);

                if (business == null)
                {
                    return null;
                }

                BusinessDTO businessDTO = _mapper.Map<BusinessDTO>(business);
                businessDTO.Beers = new List<Beer>();

                foreach (BusinessBeer busBeer in business.BusinessBeers)
                {
                    businessDTO.Beers.Add(await _beerRepository.GetBeer(busBeer.BeerId));
                }

                return businessDTO;
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
                List<Business> businesses = await _businessRepository.GetBusinesses();
                List<BusinessDTO> businessDTOs = _mapper.Map<List<BusinessDTO>>(businesses);

                for (int i = 0; i < businesses.Count; i++)
                {
                    businessDTOs[i].Beers = new List<Beer>();
                    foreach (BusinessBeer busBeer in businesses[i].BusinessBeers)
                    {
                        businessDTOs[i].Beers.Add(await _beerRepository.GetBeer(busBeer.BeerId));
                    }
                }

                return businessDTOs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddBusiness(AddBusinessDTO business)
        {
            try
            {
                Guid newBusinessGuid = Guid.NewGuid();
                List<BusinessBeer> businessBeers = new List<BusinessBeer>();

                foreach (Guid beerId in business.Beers)
                {
                    businessBeers.Add(new BusinessBeer { BeerId = beerId, BusinessId = newBusinessGuid });
                }

                Business newBusiness = _mapper.Map<Business>(business);
                newBusiness.BusinessId = newBusinessGuid;
                newBusiness.BusinessBeers = businessBeers;

                await _businessRepository.AddBusiness(newBusiness);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BusinessDTO> UpdateBusiness(UpdateBusinessDTO business)
        {
            try
            {
                List<BusinessBeer> businessBeers = new List<BusinessBeer>();

                foreach (Guid beerId in business.Beers)
                {
                    businessBeers.Add(new BusinessBeer { BeerId = beerId, BusinessId = business.BusinessId });
                }

                Business updateBusiness = _mapper.Map<Business>(business);
                updateBusiness.BusinessBeers = businessBeers;

                return _mapper.Map<BusinessDTO>(await _businessRepository.UpdateBusiness(updateBusiness));
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

        public async Task<LocationDTO> UpdateLocation(Location location)
        {
            Location updateLocation = _mapper.Map<Location>(location);

            try
            {
                return _mapper.Map<LocationDTO>(await _locationRepository.UpdateLocation(updateLocation));
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
    }
}