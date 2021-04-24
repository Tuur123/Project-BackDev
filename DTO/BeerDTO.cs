using System;
using System.Collections.Generic;
using BeerApi.Models;

namespace BeerApi.DTO
{
    public class BeerDTO
    {
        public int BeerId { get; set; }
        public string Name { get; set; }
        public double AlchoholPercentage { get; set; }
        public string Brewer { get; set; }
        public List<int> Businesses { get; set; }
    }
}
