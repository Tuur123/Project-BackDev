using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeerApi.Models;

namespace BeerApi.DTO
{
    public class UpdateBusinessDTO
    {
        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public Guid LocationId { get; set; }

        [Required]
        public List<Guid> Beers { get; set; }
    }
}
