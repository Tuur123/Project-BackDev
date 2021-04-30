using System;
using System.Collections.Generic;
using BeerApi.Models;

namespace BeerApi.DTO
{
    public class BeerDTO
    {
        public Guid BeerId {get; set; }
        public string Name { get; set; }
        public double AlchoholPercentage { get; set; }
        public string Brewer { get; set; }
    }
}
