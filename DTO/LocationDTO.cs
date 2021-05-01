using System;
using System.ComponentModel.DataAnnotations;

namespace BeerApi.DTO
{
    public class LocationDTO
    {
        [Required]
        public string City { get; set; }
        public int Postcode { get; set; }
        public Guid LocationId { get; set; }
    }
}
