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
        public Guid LocationId { get; set; }
        public List<Guid> Beers { get; set; }
    }
}
