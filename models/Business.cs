using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BeerApi.Models
{
    public class Business
    {
        public int BusinessId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public int LocationId { get; set; }
        public virtual List<BusinessBeer> BusinessBeers {get; set;}
    }
}
