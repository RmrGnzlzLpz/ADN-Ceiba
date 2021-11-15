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
    public class VehiclesControllerTest
    {
        readonly WebapiAppFactory<Startup> _appFactory;
        readonly string _license1;
        readonly string _license2;

        public VehiclesControllerTest()
        {
            _appFactory = new WebapiAppFactory<Startup>();
            _license1 = "ABC126";
            _license2 = "ABC457";
            SeedDataBase(_appFactory.Services);
        }

        void SeedDataBase(IServiceProvider provider)
        {
            using var scope = provider.CreateScope();
            var _vehicleRepository = scope.ServiceProvider
                .GetRequiredService<IGenericRepository<Vehicle>>();
            _ = _vehicleRepository.AddAsync(new Vehicle(_license1, 500, Domain.Enums.VehicleType.Car)
            {
                EntryDate = new DateTime(2021, 09, 01),
                IsParked = true
            }).Result;
            _ = _vehicleRepository.AddAsync(new Vehicle(_license2, 500, Domain.Enums.VehicleType.Motorcycle)
            {
                EntryDate = new DateTime(2021, 09, 01),
                IsParked = true
            }).Result;
        }

        [TestMethod]
        public async Task FindVehicleSuccess()
        {
            var client = _appFactory.CreateClient();
            var response = await client.GetAsync($"api/Vehicles/History/{_license1}");
            response.EnsureSuccessStatusCode();

            var vehicle = JsonSerializer.Deserialize<HistoryOfVehicleDto>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );

            Assert.IsTrue(vehicle.License.Equals(_license1));
        }

        [TestMethod]
        public async Task ParkCarSuccess()
        {
            var license = $"CBD12{(int)DateTime.Now.AddDays(3).DayOfWeek}";
            var client = _appFactory.CreateClient();
            var request = new Application.Vehicle.Commands.ParkVehicle.ParkVehicleCommand(license, 500, Domain.Enums.VehicleType.Car);
            var requestContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Vehicles/Park", requestContent);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task ParkMotoSuccess()
        {
            var license = $"MBD12{(int)DateTime.Now.AddDays(3).DayOfWeek}";
            var client = _appFactory.CreateClient();
            var request = new Application.Vehicle.Commands.ParkVehicle.ParkVehicleCommand(license, 500, Domain.Enums.VehicleType.Motorcycle);
            var requestContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Vehicles/Park", requestContent);
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task TakeOutVehicleSuccess()
        {
            var license = _license1;
            var client = _appFactory.CreateClient();
            var request = new Application.Vehicle.Commands.TakeOutVehicle.TakeOutVehicleCommand(license);
            var requestContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/Vehicles/Exit", requestContent);
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task GetVehiclesSuccess()
        {
            var client = _appFactory.CreateClient();
            var response = await client.GetAsync($"api/Vehicles");
            response.EnsureSuccessStatusCode();

            var vehicles = JsonSerializer.Deserialize<IEnumerable<GetVehicleDto>>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );

            Assert.IsTrue(vehicles.Any(x => x.License == _license1));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task CalculateCostSuccess()
        {
            var client = _appFactory.CreateClient();
            var response = await client.GetAsync($"api/Vehicles/CalculateCost/" + _license2);
            response.EnsureSuccessStatusCode();

            var costDto = JsonSerializer.Deserialize<CalculateParkingCostDto>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );

            Assert.IsTrue(costDto.License == _license2);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
