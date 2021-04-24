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
            CreateMap<Location, LocationDTO>();
            CreateMap<Business, BusinessDTO>();
            CreateMap<Beer, BeerUpdateDTO>();
            CreateMap<Location, LocationUpdateDTO>();
        }
    }
}
