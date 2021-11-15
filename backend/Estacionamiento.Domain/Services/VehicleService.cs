using Estacionamiento.Domain.Entities;
using Estacionamiento.Domain.Ports;
using System;
using System.Threading.Tasks;
using System.Linq;
using Estacionamiento.Domain.Exceptions;
using Estacionamiento.Domain.Dtos;
using System.Collections.Generic;
using Estacionamiento.Domain.Enums;

namespace Estacionamiento.Domain.Services
{
    [DomainService]
    public class VehicleService : IDisposable
    {
        private readonly IGenericRepository<Vehicle> _vehicleRepository;
        private readonly IParkingConfiguration _configuration;
        public IParkingConfiguration ParkingConfiguration => _configuration;

        public VehicleService(
            IGenericRepository<Vehicle> vehicleRepository,
            IParkingConfiguration configuration
        )
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Vehicle?> GetVehicleAsync(string license)
        {
            var vehicle = (await _vehicleRepository.GetAsync(x => x.License == license)).FirstOrDefault();
            return vehicle;
        }

        public TakeOutVehicleDto CalculateParkingCost(Vehicle vehicle, DateTime date)
        {
            _ = vehicle ?? throw new ArgumentNullException(nameof(vehicle));

            (int days, int hours) = CalculateTime(vehicle.EntryDate, date);
            var cost = CalculateCost(vehicle, days, hours);
            return new TakeOutVehicleDto(hours, days, vehicle.EntryDate, date, cost, vehicle.License);
        }

        public async Task<TakeOutVehicleDto> TakeOutOfParkingAsync(Vehicle vehicle, DateTime date)
        {
            _ = vehicle ?? throw new ArgumentNullException(nameof(vehicle));

            var response = CalculateParkingCost(vehicle, date);
            vehicle.TakeOut(date);
            vehicle.NewHistory(date, response.Days, response.Hours, response.Value);

            await _vehicleRepository.UpdateAsync(vehicle);
            return response;
        }

        public async Task ParkVehicleAsync(Vehicle vehicle, DateTime date)
        {
            _ = vehicle ?? throw new ArgumentNullException(nameof(vehicle));

            if (IsPicoYPlaca(date.DayOfWeek, vehicle.License))
            {
                throw new PicoYPlacaException();
            }

            var totalVehicles = await _vehicleRepository.CountAsync(v => v.Type == vehicle.Type && v.IsParked);
            if (totalVehicles >= _configuration.TotalCells.GetValueOrDefault(vehicle.Type))
            {
                throw new NoFreeCellsException();
            }

            vehicle.Park(date);
            await _vehicleRepository.UpdateAsync(vehicle);
        }

        public async Task<Vehicle?> GetVehicleWithHistoryAsync(string license)
        {
            var vehicle = (await _vehicleRepository
                .GetAsync(x => x.License == license, includeObjectProperties: x => x.VehicleHistory))
                .FirstOrDefault();
            return vehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            var vehicles = await _vehicleRepository.GetAsync(orderBy: x => x.OrderByDescending(vehicles => vehicles.IsParked).ThenByDescending(vehicles => vehicles.EntryDate));
            return vehicles;
        }

        protected double CalculateCost(Vehicle vehicle, int days, int hours)
        {
            _ = vehicle ?? throw new ArgumentNullException(nameof(vehicle));

            return GetNetCost(days, hours, vehicle.Type) + GetExtraCostByCylinderCapacity(vehicle.CylinderCapacity, vehicle.Type);
        }

        protected (int days, int hours) CalculateTime(DateTime entryDate, DateTime endDate)
        {
            int totalTime = (int)Math.Ceiling(endDate.Subtract(entryDate).TotalHours);

            var calculatedParkingDays = totalTime / _configuration.NumberOfHoursPerDay;
            var calculatedParkingHours = totalTime % _configuration.NumberOfHoursPerDay;

            if (calculatedParkingHours >= _configuration.NumberOfHoursAsDay)
            {
                calculatedParkingDays += 1;
                calculatedParkingHours = 0;
            }
            return (calculatedParkingDays, calculatedParkingHours);
        }

        protected double GetExtraCostByCylinderCapacity(short cylinderCapacity, VehicleType vehicleType) =>
            cylinderCapacity > _configuration.ExtraCostCylinderCapacity.GetValueOrDefault(vehicleType)
            ? _configuration.ExtraCost.GetValueOrDefault(vehicleType)
            : 0.0;

        protected double GetNetCost(int days, int hours, VehicleType vehicleType) =>
            hours * _configuration.HourCost.GetValueOrDefault(vehicleType)
            + days * _configuration.DayCost.GetValueOrDefault(vehicleType);

        protected bool IsPicoYPlaca(DayOfWeek day, string license)
        {
            if (string.IsNullOrWhiteSpace(license))
            {
                throw new AppException(message: "Wrong " + nameof(license));
            }

            if (_configuration.PicoYPlaca.ContainsKey(day))
            {
                return _configuration.PicoYPlaca[day].Any(license.EndsWith);
            }
            return false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _vehicleRepository.Dispose();
            }
        }
    }
}
