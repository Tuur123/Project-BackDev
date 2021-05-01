using System;
using System.ComponentModel.DataAnnotations;

using BeerApi.Models;

namespace BeerApi.DTO
{
    public class BeerDTO
    {
        public Guid BeerId { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 70)]
        public double AlchoholPercentage { get; set; }
        public string Brewer { get; set; }
    }
}
