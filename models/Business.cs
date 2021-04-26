using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BeerApi.Models
{
    public class Business
    {
        public Guid BusinessId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        // location relationship
        [JsonIgnore]
        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        // BusinessBeer relationship
        public List<BusinessBeer> BusinessBeers { get; set; }
    }
}
