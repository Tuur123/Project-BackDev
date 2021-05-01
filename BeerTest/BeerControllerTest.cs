using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.CSharp.RuntimeBinder;

using BeerApi.DTO;

using FluentAssertions;

using Newtonsoft.Json;

using Xunit;
using System.Text;

namespace BeerTest
{
    public class BeerControllerTest : IClassFixture<WebApplicationFactory<BeerApi.Startup>>
    {
        public HttpClient client { get; set; }

        public BeerControllerTest(WebApplicationFactory<BeerApi.Startup> fixture)
        {
            client = fixture.CreateClient();
        }

        #region GET

        [Fact]
        public async void Get_Beers_Should_Return_Ok()
        {
            HttpResponseMessage response = await client.GetAsync("/api/beers");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            List<BeerDTO> beers = JsonConvert.DeserializeObject<List<BeerDTO>>(await response.Content.ReadAsStringAsync());
            Assert.True(beers.Count > 0);
        }

        [Fact]
        public async void Get_Businesses_Should_Return_Okay()
        {
            HttpResponseMessage response = await client.GetAsync("/api/businesses");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            List<BusinessDTO> businesses = JsonConvert.DeserializeObject<List<BusinessDTO>>(await response.Content.ReadAsStringAsync());
            Assert.True(businesses.Count > 0);
        }

        [Fact]
        public async void Get_Locations_Should_Return_Ok()
        {
            HttpResponseMessage response = await client.GetAsync("/api/locations");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            List<LocationDTO> locations = JsonConvert.DeserializeObject<List<LocationDTO>>(await response.Content.ReadAsStringAsync());
            Assert.True(locations.Count > 0);
        }
        #endregion GET

        #region POST

        [Fact]
        public async void Add_Beer_Should_Return_Ok()
        {
            BeerDTO beer = new BeerDTO()
            {
                Name = "Maes",
                AlchoholPercentage = 3.5,
                Brewer = "TestString"
            };

            string json = JsonConvert.SerializeObject(beer);
            HttpResponseMessage response = await client.PostAsync("/api/beers", new StringContent(json, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            BeerDTO createdBeer = JsonConvert.DeserializeObject<BeerDTO>(await response.Content.ReadAsStringAsync());
            Assert.True("Maes" == createdBeer.Name);
            Assert.Equal<double>(3.5, createdBeer.AlchoholPercentage);
            Assert.True("TestString" == createdBeer.Brewer);

        }

        [Fact]
        public async void Add_Business_Should_Return_Ok()
        {

            HttpResponseMessage locResponse = await client.GetAsync("/api/locations");
            List<LocationDTO> locations = JsonConvert.DeserializeObject<List<LocationDTO>>(await locResponse.Content.ReadAsStringAsync());
            HttpResponseMessage beerResponse = await client.GetAsync("/api/beers");
            List<BeerDTO> beers = JsonConvert.DeserializeObject<List<BeerDTO>>(await beerResponse.Content.ReadAsStringAsync());

            AddBusinessDTO business = new AddBusinessDTO()
            {
                Name = "Test Business",
                Type = "Test",
                Email = "test.berdrijf@gmail.com",
                LocationId = locations[0].LocationId,
                Beers = new List<Guid>() { beers[0].BeerId, beers[2].BeerId }
            };

            string json = JsonConvert.SerializeObject(business);
            HttpResponseMessage response = await client.PostAsync("/api/businesses", new StringContent(json, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            BusinessDTO createdbusiness = JsonConvert.DeserializeObject<BusinessDTO>(await response.Content.ReadAsStringAsync());
            Assert.True("Test Business" == createdbusiness.Name);
            Assert.True("Test" == createdbusiness.Type);
            Assert.True("test.berdrijf@gmail.com" == createdbusiness.Email);
        }

        [Fact]
        public async void Add_Location_Should_Return_Ok()
        {
            LocationDTO location = new LocationDTO()
            {
                City = "Schoten",
                Postcode = 2900,
            };

            string json = JsonConvert.SerializeObject(location);
            HttpResponseMessage response = await client.PostAsync("/api/locations", new StringContent(json, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            LocationDTO createdLocation = JsonConvert.DeserializeObject<LocationDTO>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(createdLocation);
            Assert.Equal<int>(2900, createdLocation.Postcode);
            Assert.True(createdLocation.City == "Schoten");
        }
        #endregion POST

        #region PUT

        [Fact]
        public async void Update_Beer_Should_Return_Ok()
        {
            HttpResponseMessage beerResponse = await client.GetAsync("/api/beers");
            List<BeerDTO> beers = JsonConvert.DeserializeObject<List<BeerDTO>>(await beerResponse.Content.ReadAsStringAsync());

            BeerDTO beer = new BeerDTO()
            {
                BeerId = beers[0].BeerId,
                Name = "Maes_updated",
                AlchoholPercentage = 7,
                Brewer = "TestString_updated"
            };

            string json = JsonConvert.SerializeObject(beer);
            HttpResponseMessage response = await client.PutAsync("/api/beers", new StringContent(json, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            BeerDTO createdBeer = JsonConvert.DeserializeObject<BeerDTO>(await response.Content.ReadAsStringAsync());
            Assert.True("Maes_updated" == createdBeer.Name);
            Assert.True("TestString_updated" == createdBeer.Brewer);
            Assert.Equal<double>(7, createdBeer.AlchoholPercentage);
        }

        [Fact]
        public async void Update_Business_Should_Return_Ok()
        {

            HttpResponseMessage locResponse = await client.GetAsync("/api/locations");
            List<LocationDTO> locations = JsonConvert.DeserializeObject<List<LocationDTO>>(await locResponse.Content.ReadAsStringAsync());
            HttpResponseMessage beerResponse = await client.GetAsync("/api/beers");
            List<BeerDTO> beers = JsonConvert.DeserializeObject<List<BeerDTO>>(await beerResponse.Content.ReadAsStringAsync());
            HttpResponseMessage busResponse = await client.GetAsync("/api/businesses");
            List<BusinessDTO> businesses = JsonConvert.DeserializeObject<List<BusinessDTO>>(await busResponse.Content.ReadAsStringAsync());

            UpdateBusinessDTO business = new UpdateBusinessDTO()
            {
                BusinessId = businesses[0].BusinessId,
                Name = "Test Business_updated",
                Type = "Test_updated",
                Email = "test.updated@gmail.com",
                LocationId = locations[0].LocationId,
                Beers = new List<Guid>() { beers[0].BeerId, beers[2].BeerId }
            };

            string json = JsonConvert.SerializeObject(business);
            HttpResponseMessage response = await client.PutAsync("/api/businesses", new StringContent(json, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            BusinessDTO createdbusiness = JsonConvert.DeserializeObject<BusinessDTO>(await response.Content.ReadAsStringAsync());
            Assert.True("Test Business_updated" == createdbusiness.Name);
            Assert.True("Test_updated" == createdbusiness.Type);
            Assert.True("test.updated@gmail.com" == createdbusiness.Email);
        }

        [Fact]
        public async void Update_Location_Should_Return_Ok()
        {
            HttpResponseMessage locResponse = await client.GetAsync("/api/locations");
            List<LocationDTO> locations = JsonConvert.DeserializeObject<List<LocationDTO>>(await locResponse.Content.ReadAsStringAsync());

            LocationDTO location = new LocationDTO()
            {
                LocationId = locations[0].LocationId,
                City = "Anderlecht",
                Postcode = 1070,
            };

            string json = JsonConvert.SerializeObject(location);
            HttpResponseMessage response = await client.PutAsync("/api/locations", new StringContent(json, Encoding.UTF8, "application/json"));

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            LocationDTO createdLocation = JsonConvert.DeserializeObject<LocationDTO>(await response.Content.ReadAsStringAsync());
            Assert.Equal<int>(1070, createdLocation.Postcode);
            Assert.True("Anderlecht" == createdLocation.City);
        }
        #endregion PUT

        #region DELETE

        [Fact]
        public async void Delete_Beer_Should_Return_Ok()
        {
            HttpResponseMessage beerResponse = await client.GetAsync("/api/beers");
            List<BeerDTO> beers = JsonConvert.DeserializeObject<List<BeerDTO>>(await beerResponse.Content.ReadAsStringAsync());

            Guid beerToDelete = beers[0].BeerId;

            HttpResponseMessage response = await client.DeleteAsync(string.Format("/api/beers/{0}", beerToDelete));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage checkResponse = await client.GetAsync(string.Format("/api/beers/{0}", beerToDelete));
            checkResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Delete_Business_Should_Return_Okay()
        {
            HttpResponseMessage businessResponse = await client.GetAsync("/api/businesses");
            List<BusinessDTO> businesses = JsonConvert.DeserializeObject<List<BusinessDTO>>(await businessResponse.Content.ReadAsStringAsync());

            Guid businessToDelete = businesses[0].BusinessId;

            HttpResponseMessage response = await client.DeleteAsync(string.Format("/api/businesses/{0}", businessToDelete));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage checkResponse = await client.GetAsync(string.Format("/api/businesses/{0}", businessToDelete));
            checkResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Delete_Location_Should_Return_Ok()
        {
            HttpResponseMessage businessResponse = await client.GetAsync("/api/locations");
            List<LocationDTO> locations = JsonConvert.DeserializeObject<List<LocationDTO>>(await businessResponse.Content.ReadAsStringAsync());

            Guid locationToDelete = locations[0].LocationId;

            HttpResponseMessage response = await client.DeleteAsync(string.Format("/api/locations/{0}", locationToDelete));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            HttpResponseMessage checkResponse = await client.GetAsync(string.Format("/api/locations/{0}", locationToDelete));
            checkResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        #endregion DELETE
    }
}
