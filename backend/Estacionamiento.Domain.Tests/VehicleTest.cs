using Estacionamiento.Domain.Entities;
using Estacionamiento.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Estacionamiento.Domain.Tests
{
    [TestClass]
    public class VehicleTest
    {
        [TestMethod]
        public void ParkVehicle()
        {
            string license = "ABC123";
            short cylinder = 500;
            VehicleType vehicleType = VehicleType.Car;

            var car = new Vehicle(license, cylinder, vehicleType);

            Assert.IsTrue(!car.IsParked);
            car.Park(DateTime.Now);
            Assert.IsTrue(car.IsParked);
        }

        [TestMethod]
        public void TakeOutVehicle()
        {
            string license = "ABC123";
            short cylinder = 500;
            VehicleType vehicleType = VehicleType.Car;

            var car = new Vehicle(license, cylinder, vehicleType);

            Assert.IsTrue(!car.IsParked);
            car.Park(DateTime.Now);
            car.TakeOut(DateTime.Now);
            Assert.IsTrue(!car.IsParked);
        }
    }
}
