using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeerApi.Models;

namespace BeerApi.DTO
{
    public class AddBusinessDTO
    {
        [Required]
        public string Name { get; set; }
        public string Type { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public Guid LocationId { get; set; }
        public List<Guid> Beers { get; set; }
    }
}
