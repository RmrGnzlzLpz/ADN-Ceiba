using Estacionamiento.Application.Vehicle.Queries;
using Estacionamiento.Domain.Entities;
using Estacionamiento.Domain.Ports;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Estacionamiento.Api.Tests
{
    [TestClass]
    public class ParkingControllerTest
    {
        readonly WebapiAppFactory<Startup> _appFactory;

        public ParkingControllerTest()
        {
            _appFactory = new WebapiAppFactory<Startup>();
        }

        [TestMethod]
        public async Task GetConfigurationSuccess()
        {
            var client = _appFactory.CreateClient();
            var response = await client.GetAsync($"api/Parking/Configuration");
            response.EnsureSuccessStatusCode();

            var parking = JsonSerializer.Deserialize<Application.Parking.Queries.ParkingDto>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
            Assert.IsNotNull(parking);
        }
    }
}
