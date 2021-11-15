using Estacionamiento.Domain.Enums;
using Estacionamiento.Domain.Exceptions;
using Estacionamiento.Domain.Ports;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Estacionamiento.Infrastructure.Adapters
{
    public class ParkingConfiguration : IParkingConfiguration
    {
        public byte NumberOfHoursAsDay { get; set; }
        public byte NumberOfHoursPerDay { get; set; }
        public Dictionary<VehicleType, uint> TotalCells { get; } = new();
        public Dictionary<VehicleType, double> HourCost { get; } = new();
        public Dictionary<VehicleType, double> DayCost { get; } = new();
        public Dictionary<VehicleType, double> ExtraCost { get; } = new();
        public Dictionary<VehicleType, ushort> ExtraCostCylinderCapacity { get; } = new();
        public Dictionary<DayOfWeek, string[]> PicoYPlaca { get; } = new();

        private const byte _defaultNumberOfHoursAsDay = 9;
        private const byte _defaultNumberOfHoursPerDay = 24;

        public ParkingConfiguration(IConfiguration configuration)
        {
            _ = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
            var parkingSection = configuration.GetSection("ParkingConfiguration");

            HourCost.Add(VehicleType.Motorcycle, parkingSection.GetValue<double>("HourMotorcycleCost"));
            HourCost.Add(VehicleType.Car, parkingSection.GetValue<double>("HourCarCost"));
            DayCost.Add(VehicleType.Motorcycle, parkingSection.GetValue<double>("DayMotorcycleCost"));
            DayCost.Add(VehicleType.Car, parkingSection.GetValue<double>("DayCarCost"));
            ExtraCost.Add(VehicleType.Motorcycle, parkingSection.GetValue<double>("ExtraMotorcycleCost"));
            ExtraCostCylinderCapacity.Add(VehicleType.Motorcycle, parkingSection.GetValue<ushort>("ExtraCostCylinderCapacity"));
            TotalCells.Add(VehicleType.Car, parkingSection.GetValue<uint>("TotalCellsForCars"));
            TotalCells.Add(VehicleType.Motorcycle, parkingSection.GetValue<uint>("TotalCellsForMotorcycles"));
            NumberOfHoursAsDay = parkingSection.GetValue<byte>("NumberOfHoursAsDay");
            NumberOfHoursPerDay = parkingSection.GetValue<byte>("NumberOfHoursPerDay");

            AddPicoYPlaca(parkingSection.GetSection("PicoYPlaca"));
            ValidateInitialization();
        }

        private void ValidateInitialization()
        {
            if (NumberOfHoursAsDay < 0)
            {
                NumberOfHoursAsDay = _defaultNumberOfHoursAsDay;
            }

            if (NumberOfHoursPerDay < 1)
            {
                NumberOfHoursPerDay = _defaultNumberOfHoursPerDay;
            }
        }

        private void AddPicoYPlaca(IConfigurationSection section)
        {
            PicoYPlaca.Add(DayOfWeek.Sunday, GetDayPicoYPlaca(section, DayOfWeek.Sunday));
            PicoYPlaca.Add(DayOfWeek.Monday, GetDayPicoYPlaca(section, DayOfWeek.Monday));
            PicoYPlaca.Add(DayOfWeek.Tuesday, GetDayPicoYPlaca(section, DayOfWeek.Tuesday));
            PicoYPlaca.Add(DayOfWeek.Wednesday, GetDayPicoYPlaca(section, DayOfWeek.Wednesday));
            PicoYPlaca.Add(DayOfWeek.Thursday, GetDayPicoYPlaca(section, DayOfWeek.Thursday));
            PicoYPlaca.Add(DayOfWeek.Friday, GetDayPicoYPlaca(section, DayOfWeek.Friday));
            PicoYPlaca.Add(DayOfWeek.Saturday, GetDayPicoYPlaca(section, DayOfWeek.Saturday));
        }

        private static string[] GetDayPicoYPlaca(IConfigurationSection section, DayOfWeek dayOfWeek)
        {
            return section.GetValue<string>(dayOfWeek.ToString()).Split(',', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
