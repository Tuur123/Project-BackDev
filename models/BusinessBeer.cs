using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BeerApi.Models
{
    public class BusinessBeer
    {
        public Guid BusinessId { get; set; }
        public Guid BeerId { get; set; }
        public Beer Beer { get; set; }
    }
}
