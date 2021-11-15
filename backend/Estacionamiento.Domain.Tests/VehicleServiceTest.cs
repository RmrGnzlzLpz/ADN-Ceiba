using Microsoft.VisualStudio.TestTools.UnitTesting;
using Estacionamiento.Domain.Ports;
using Estacionamiento.Domain.Entities;
using NSubstitute;
using Estacionamiento.Domain.Services;
using System.Threading.Tasks;
using Estacionamiento.Domain.Exceptions;
using System;
using System.Collections.Generic;
using Estacionamiento.Domain.Enums;
using System.Linq;

namespace Estacionamiento.Domain.Tests
{
    [TestClass]
    public class VehicleServiceTest
    {

        IGenericRepository<Vehicle> _vehicleRepository;
        VehicleService _vehicleService;
        IParkingConfiguration _configuration;

        [TestInitialize]
        public void Init()
        {
            _vehicleRepository = Substitute.For<IGenericRepository<Vehicle>>();
            var parkingConfiguration = Substitute.For<IParkingConfiguration>();

            parkingConfiguration.HourCost.Returns(new Dictionary<VehicleType, double>()
            {
                {VehicleType.Motorcycle, 500 },
                {VehicleType.Car, 1000 },
            });

            parkingConfiguration.DayCost.Returns(new Dictionary<VehicleType, double>()
            {
                {VehicleType.Motorcycle, 4000 },
                {VehicleType.Car, 8000 },
            });

            parkingConfiguration.TotalCells.Returns(new Dictionary<VehicleType, uint>()
            {
                {VehicleType.Motorcycle, 10 },
                {VehicleType.Car, 20 },
            });

            parkingConfiguration.ExtraCost.Returns(new Dictionary<VehicleType, double>()
            {
                {VehicleType.Motorcycle, 2000 }
            });

            parkingConfiguration.ExtraCostCylinderCapacity.Returns(new Dictionary<VehicleType, ushort>()
            {
                {VehicleType.Motorcycle, 500 }
            });

            parkingConfiguration.NumberOfHoursAsDay.Returns((byte)9);
            parkingConfiguration.NumberOfHoursPerDay.Returns((byte)24);

            parkingConfiguration.PicoYPlaca.Returns(new Dictionary<DayOfWeek, string[]>()
            {
                {DayOfWeek.Sunday, new string[]{""}},
                {DayOfWeek.Monday, new string[]{"0","1"}},
                {DayOfWeek.Tuesday, new string[]{"2","3"}},
                {DayOfWeek.Wednesday, new string[]{"4","5"}},
                {DayOfWeek.Thursday, new string[]{"6","7"}},
                {DayOfWeek.Friday, new string[]{"8","9"}},
                {DayOfWeek.Saturday, new string[]{""}}
            });

            _configuration = parkingConfiguration;
            _vehicleService = new(_vehicleRepository, _configuration);
        }

        [TestMethod]
        public async Task FailedToTakeOutVehicle()
        {
            bool callFailed = false;
            try
            {
                var license = "ABC123";
                var vehicle = new Vehicle(license, 500, VehicleType.Car);
                vehicle.ExitDate = DateTime.Now;

                await _vehicleService.TakeOutOfParkingAsync(vehicle, DateTime.Now);
            }
            catch (NotParkedException)
            {
                callFailed = true;
            }

            Assert.IsTrue(callFailed, $"Expected call to FailedToTakeOutVehicle to fail with {nameof(NotParkedException)}");
        }

        [TestMethod]
        public async Task SuccessTakeOutVehicle()
        {
            var license = "ABC123";
            var vehicle = new Vehicle(license, 500, VehicleType.Car)
            {
                EntryDate = DateTime.Now.AddDays(-1),
                IsParked = true
            };

            Assert.IsTrue(vehicle.IsParked);

            await _vehicleService.TakeOutOfParkingAsync(vehicle, DateTime.Now);

            Assert.IsTrue(!vehicle.IsParked);
        }

        [TestMethod]
        public async Task FindVehicleSuccess()
        {
            var license = "ABC123";
            Vehicle vehicle = new(license, 500, VehicleType.Car);

            _vehicleRepository.GetAsync().ReturnsForAnyArgs(new List<Vehicle> { vehicle });

            var vehicleResponse = await _vehicleService.GetVehicleAsync(vehicle.License);

            Assert.AreEqual(license, vehicleResponse.License);
        }

        [TestMethod]
        public void CalculateParkingCostSuccess()
        {
            var days = 1;
            var hours = 2;
            var minutes = 1;
            var valueExpected = 11000;
            var license = "ABC123";
            var vehicle = new Vehicle(license, 500, VehicleType.Car)
            {
                EntryDate = DateTime.Now.AddDays(-days).AddHours(-hours).AddMinutes(-minutes)
            };

            var response = _vehicleService.CalculateParkingCost(vehicle, DateTime.Now);

            Assert.AreEqual(valueExpected, response.Value);
        }

        [TestMethod]
        public async Task VehicleParkingNoActiveFail()
        {
            bool callFailed = false;
            try
            {

                var license = "ABC123";
                var vehicle = new Vehicle(license, 500, VehicleType.Car)
                {
                    EntryDate = DateTime.Now.AddDays(-1),
                    ExitDate = DateTime.Now
                };

                await _vehicleService.TakeOutOfParkingAsync(vehicle, DateTime.Now);
            }
            catch (NotParkedException)
            {
                callFailed = true;
            }

            Assert.IsTrue(callFailed, $"Expected call to VehicleParkingNoActive to fail with {nameof(NotParkedException)}");
        }

        [TestMethod]
        public async Task CalculateCarParkingValueSuccess()
        {
            var days = 1;
            var hours = 2;
            var minutes = 1;
            var valueExpected = 11000;
            var license = "ABC123";
            var vehicle = new Vehicle(license, 500, VehicleType.Car)
            {
                EntryDate = DateTime.Now.AddDays(-days).AddHours(-hours).AddMinutes(-minutes),
                IsParked = true
            };

            var response = await _vehicleService.TakeOutOfParkingAsync(vehicle, DateTime.Now);

            Assert.AreEqual(valueExpected, response.Value);
        }

        [TestMethod]
        public async Task CalculateMotorcycleParkingValueSuccess()
        {
            var hours = 9;
            var minutes = 59;
            var valueExpected = 6000;
            var license = "ABC12B";
            var vehicle = new Vehicle(license, 650, VehicleType.Motorcycle)
            {
                EntryDate = DateTime.Now.AddHours(-hours).AddMinutes(-minutes),
                IsParked = true
            };

            var response = await _vehicleService.TakeOutOfParkingAsync(vehicle, DateTime.Now);

            Assert.AreEqual(valueExpected, response.Value);
        }

        [DataRow("ABC120", 2021, 09, 06)]
        [DataRow("ABC121", 2021, 09, 06)]
        [DataRow("ABC122", 2021, 09, 07)]
        [DataRow("ABC123", 2021, 09, 07)]
        [DataRow("ABC124", 2021, 09, 08)]
        [DataRow("ABC125", 2021, 09, 08)]
        [DataRow("ABC126", 2021, 09, 09)]
        [DataRow("ABC127", 2021, 09, 09)]
        [DataRow("ABC128", 2021, 09, 10)]
        [DataRow("ABC129", 2021, 09, 10)]
        [TestMethod]
        public async Task ParkVehicleWithPicoYPlacaFail(string license, int year, int month, int day)
        {
            bool callFailed = false;
            try
            {
                var vehicle = new Vehicle(license, 650, VehicleType.Motorcycle)
                {
                    ExitDate = DateTime.Now
                };

                var date = new DateTime(year, month, day);

                await _vehicleService.ParkVehicleAsync(vehicle, date);
            }
            catch (PicoYPlacaException)
            {
                callFailed = true;
            }

            Assert.IsTrue(callFailed, $"Expected call to ParkVehicleWithPicoYPlaca to fail with {nameof(NotParkedException)}");
        }

        [TestMethod]
        [DataRow("ABC129", 2021, 09, 06)]
        [DataRow("ABC128", 2021, 09, 06)]
        [DataRow("ABC125", 2021, 09, 07)]
        [DataRow("ABC121", 2021, 09, 07)]
        [DataRow("ABC127", 2021, 09, 08)]
        [DataRow("ABC126", 2021, 09, 08)]
        [DataRow("ABC123", 2021, 09, 09)]
        [DataRow("ABC122", 2021, 09, 09)]
        [DataRow("ABC124", 2021, 09, 10)]
        [DataRow("ABC120", 2021, 09, 10)]
        public async Task ParkVehicleWithoutPicoYPlacaSuccess(string license, int year, int month, int day)
        {
            var car = new Vehicle(license, 500, VehicleType.Car);
            var motorcycle = new Vehicle(license, 650, VehicleType.Motorcycle);

            var date = new DateTime(year, month, day);

            await _vehicleService.ParkVehicleAsync(car, date);
            await _vehicleService.ParkVehicleAsync(motorcycle, date);

            Assert.IsTrue(car.IsParked);
            Assert.IsTrue(motorcycle.IsParked);
        }

        [TestMethod]
        public async Task GetVehicleWithHistorySuccess()
        {
            var license = "ABC123";
            var date = DateTime.Now;
            var vehicle = new Vehicle(license, 500, VehicleType.Car);

            vehicle.Park(date);
            vehicle.TakeOut(date.AddDays(1));
            vehicle.NewHistory(date.AddDays(1), 1, 0, 8000);

            _vehicleRepository.GetAsync(includeObjectProperties: x => x.VehicleHistory)
                .ReturnsForAnyArgs(new List<Vehicle> { vehicle });

            var vehicleWithHistory = await _vehicleService.GetVehicleWithHistoryAsync(license);

            Assert.IsTrue(vehicleWithHistory.VehicleHistory.Any(x => x.EntryDate == date));
            Assert.IsTrue(vehicleWithHistory.VehicleHistory.Any(x => x.ExitDate == date.AddDays(1)));
        }

        [TestMethod]
        public async Task ParkWithOccupiedCellsFail()
        {
            var callFailed = false;
            try
            {
                _configuration.TotalCells[VehicleType.Car] = 2;
                var car1 = new Vehicle("ABC123", 500, VehicleType.Car);
                _vehicleRepository.CountAsync(x => x.Type == VehicleType.Car).ReturnsForAnyArgs(2);
                await _vehicleService.ParkVehicleAsync(car1, new DateTime(2021, 09, 01));
            }
            catch (NoFreeCellsException)
            {
                callFailed = true;
            }

            Assert.IsTrue(callFailed, $"Expected call to ParkWithOccupiedCellsFail to fail with {nameof(NoFreeCellsException)}");
        }
    }
}
