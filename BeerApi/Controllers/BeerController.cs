using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using BeerApi.DTO;
using BeerApi.Models;
using BeerApi.Services;

namespace BeerApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class BeerController : ControllerBase
    {
        private readonly IBeerService _beerService;
        private readonly ILogger<BeerController> _logger;

        public BeerController(ILogger<BeerController> logger, IBeerService beerService)
        {
            _beerService = beerService;
            _logger = logger;
        }

        #region Beers

        [HttpGet]
        [Route("beers/{beerId}")]
        public async Task<ActionResult<BeerDTO>> GetBeers(Guid beerId)
        {
            try
            {
                BeerDTO beer = await _beerService.GetBeer(beerId);

                if (beer != null)
                {
                    return new OkObjectResult(beer);
                }
                else
                {
                    return new StatusCodeResult(400);
                }

            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("beers")]
        public async Task<ActionResult<List<BeerDTO>>> GetBeers()
        {
            try
            {
                List<BeerDTO> beers = await _beerService.GetBeers();

                if (beers != null)
                {
                    return new OkObjectResult(beers);
                }
                else
                {
                    return new StatusCodeResult(400);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("beers")]
        public async Task<ActionResult> AddBeer(BeerDTO beer)
        {
            try
            {
                return new OkObjectResult(await _beerService.AddBeer(beer));
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("beers")]
        public async Task<ActionResult> UpdateBeer(BeerDTO beer)
        {
            try
            {
                BeerDTO updatedBeer = await _beerService.UpdateBeer(beer);
                if (updatedBeer != null)
                {
                    return new OkObjectResult(updatedBeer);
                }
                return new StatusCodeResult(400);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete]
        [Route("beers/{beerId}")]
        public async Task<ActionResult> DeleteBeer(Guid beerId)
        {
            try
            {
                if (await _beerService.DeleteBeer(beerId) == null)
                {
                    return new StatusCodeResult(400);
                }
                else
                {
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        #endregion Beers

        #region Businesses

        [HttpGet]
        [Route("businesses/{businessId}")]
        public async Task<ActionResult<BusinessDTO>> GetBusiness(Guid businessId)
        {
            try
            {
                BusinessDTO business = await _beerService.GetBusiness(businessId);
                if (business != null)
                {
                    return new OkObjectResult(business);
                }
                else
                {
                    return new StatusCodeResult(400);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("businesses")]
        public async Task<ActionResult<List<BusinessDTO>>> GetBusinesses()
        {
            try
            {
                List<BusinessDTO> businesses = await _beerService.GetBusinesses();
                if (businesses != null)
                {
                    return new OkObjectResult(businesses);
                }
                else
                {
                    return new StatusCodeResult(400);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("businesses")]
        public async Task<ActionResult> AddBusiness(AddBusinessDTO business)
        {
            try
            {
                BusinessDTO newBusiness = await _beerService.AddBusiness(business);
                if (newBusiness != null)
                {
                    return new OkObjectResult(newBusiness);
                }
                else
                {
                    return new StatusCodeResult(400);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("businesses")]
        public async Task<ActionResult> UpdateBusiness(UpdateBusinessDTO business)
        {
            try
            {
                BusinessDTO updatedBusiness = await _beerService.UpdateBusiness(business);
                if (updatedBusiness != null)
                {
                    return new OkObjectResult(updatedBusiness);
                }
                else
                {
                    return new StatusCodeResult(400);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete]
        [Route("businesses/{businessId}")]
        public async Task<ActionResult> DeleteBusiness(Guid businessId)
        {
            try
            {
                if (await _beerService.DeleteBusiness(businessId) == null)
                {
                    return new StatusCodeResult(400);
                }
                else
                {
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        #endregion Businesses

        #region Location

        [HttpGet]
        [Route("locations/{locationId}")]
        public async Task<ActionResult<LocationDTO>> GetLocation(Guid locationId)
        {
            try
            {
                LocationDTO location = await _beerService.GetLocation(locationId);

                if (location != null)
                {
                    return new OkObjectResult(location);
                }
                else
                {
                    return new StatusCodeResult(400);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("locations")]
        public async Task<ActionResult<List<Location>>> GetLocations()
        {
            try
            {
                List<LocationDTO> locations = await _beerService.GetLocations();

                if (locations != null)
                {
                    return new OkObjectResult(locations);
                }
                else
                {
                    return new StatusCodeResult(400);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("locations")]
        public async Task<ActionResult> AddLocation(LocationDTO location)
        {
            try
            {
                LocationDTO newLocation = await _beerService.AddLocation(location);
                if (newLocation != null)
                {
                    return new OkObjectResult(newLocation);
                }
                else
                {
                    return new StatusCodeResult(400);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("locations")]
        public async Task<ActionResult> UpdateLocation(Location location)
        {
            try
            {
                LocationDTO updatedLocation = await _beerService.UpdateLocation(location);
                if (updatedLocation != null)
                {
                    return new OkObjectResult(updatedLocation);
                }
                else
                {
                    return new StatusCodeResult(400);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete]
        [Route("locations/{locationId}")]
        public async Task<ActionResult> DeleteLocation(Guid locationId)
        {
            try
            {
                if (await _beerService.DeleteLocation(locationId) == null)
                {
                    return new StatusCodeResult(400);
                }
                else
                {
                    return new StatusCodeResult(200);
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        #endregion Location
    }
}
