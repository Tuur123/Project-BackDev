using System;

namespace BeerApi.Models
{
    public class BusinessBeer
    {
        public int BeerId { get; set; }
        public Beer Beer { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }
    }
}
