using Microsoft.CSharp.RuntimeBinder;
using System;
using Xunit;
using BeerApi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BeerTest
{
    public class BeerControllerTest : IClassFixture<WebApplicationFactory<BeerApi.Startup>>
    {

        [Fact]
        public void Get_Beers_Should_Return_Ok()
        {
            
        }
    }
}
