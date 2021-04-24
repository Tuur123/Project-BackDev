using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BeerApi.Models
{
    public class Location
    {
        public int LocationId { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int Postcode { get; set; }
        public string Street { get; set; }

        [Range(0, 1000)] // geen negatieve huisnummers
        public int HouseNumber { get; set; }
    }
}
