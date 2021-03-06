using System;
using System.Collections.Generic;
using BeerApi.Models;

namespace BeerApi.DTO
{
    public class BusinessDTO
    {
        public Guid BusinessId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }
        public Location Location { get; set; }
        public List<Beer> Beers { get; set; }
    }
}
