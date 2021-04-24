using System;

using BeerApi.Repositories;

namespace BeerApi.Services
{
    public class BeerService
    {
        private IBeerRepository _beerRepository;
        private ILocationRepository _locationRepository;
        private IBusinessRepository _businessRepository;
    }
}
