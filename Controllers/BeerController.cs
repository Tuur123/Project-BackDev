using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeerApi.Models;
using BeerApi.DTO;
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
                return new OkObjectResult(await _beerService.GetBeer(beerId));
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
                return new OkObjectResult(await _beerService.GetBeers());
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
                await _beerService.AddBeer(beer);
                return new OkObjectResult(200);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("beers/{beerId}")]
        public async Task<ActionResult> UpdateBeer(BeerUpdateDTO beer)
        {
            try
            {
                await _beerService.UpdateBeer(beer);
                return new OkObjectResult(200);
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
                await _beerService.DeleteBeer(beerId);
                return new OkObjectResult(200);
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
                return new OkObjectResult(await _beerService.GetBusiness(businessId));
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
                return new OkObjectResult(await _beerService.GetBusinesses());
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("businesses")]
        public async Task<ActionResult> AddBusiness(BusinessDTO business)
        {
            try
            {
                await _beerService.AddBusiness(business);
                return new OkObjectResult(200);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("businesses/{businessId}")]
        public async Task<ActionResult> UpdateBusiness(BusinessDTO business)
        {
            try
            {
                await _beerService.UpdateBusiness(business);
                return new OkObjectResult(200);
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
                await _beerService.DeleteBusiness(businessId);
                return new OkObjectResult(200);
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
                return new OkObjectResult(await _beerService.GetLocation(locationId));
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
                return new OkObjectResult(await _beerService.GetLocations());
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
                await _beerService.AddLocation(location);
                return new OkObjectResult(200);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPut]
        [Route("locations/{locationId}")]
        public async Task<ActionResult> UpdateLocation(LocationUpdateDTO location)
        {
            try
            {
                await _beerService.UpdateLocation(location);
                return new OkObjectResult(200);
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
                await _beerService.DeleteLocation(locationId);
                return new OkObjectResult(200);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        #endregion Location
    }
}
