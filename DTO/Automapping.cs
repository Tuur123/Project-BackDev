using System;
using AutoMapper;
using BeerApi.Models;

namespace BeerApi.DTO
{
    public class Automapping : Profile
    {
        public Automapping()
        {
            CreateMap<Beer, BeerDTO>();
            CreateMap<BeerDTO, Beer>();
            CreateMap<BusinessBeer, BusinessBeerDTO>();
            CreateMap<Location, LocationDTO>();
            CreateMap<LocationDTO, Location>();
            CreateMap<Business, BusinessDTO>();
            CreateMap<BusinessDTO, Business>();
        }
    }
}
