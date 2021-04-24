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
        public List<int> Beers { get; set; }
    }
}
