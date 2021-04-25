using System;
using System.Collections.Generic;
using BeerApi.Models;

namespace BeerApi.DTO
{
    public class BeerDTO
    {
        public string Name { get; set; }
        public double AlchoholPercentage { get; set; }
        public string Brewer { get; set; }
        public List<BusinessBeer> BusinessBeers { get; set; }
    }

    public class BeerUpdateDTO
    {
        public Guid BeerId { get; set; }
        public string Name { get; set; }
        public double AlchoholPercentage { get; set; }
        public string Brewer { get; set; }
        public List<Guid> Businesses { get; set; }
    }
}
