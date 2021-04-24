using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BeerApi.Models
{
    public class Beer
    {
        public Guid BeerId {get; set;}

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, 70)]
        public double AlchoholPercentage { get; set; }
        public string Brewer { get; set; }
        public virtual List<BusinessBeer> BusinessBeers { get; set; }
    }
}
