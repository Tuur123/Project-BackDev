using System;

namespace BeerApi.DTO
{
    public class LocationDTO
    {
        public string City { get; set; }
        public int Postcode { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
    }

    public class LocationUpdateDTO
    {
        public Guid LocationId { get; set; }
        public string City { get; set; }
        public int Postcode { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
    }
}
