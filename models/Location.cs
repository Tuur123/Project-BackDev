using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BeerApi.Models
{
    public class Location
    {
        public Guid LocationId { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int Postcode { get; set; }
    }
}
