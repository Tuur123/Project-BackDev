using System;
using System.Collections.Generic;
using BeerApi.Models;

namespace BeerApi.DTO
{
    public class BusinessDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public Location Location { get; set; }
        public List<BusinessBeerDTO> BusinessBeers { get; set; }
    }
}
