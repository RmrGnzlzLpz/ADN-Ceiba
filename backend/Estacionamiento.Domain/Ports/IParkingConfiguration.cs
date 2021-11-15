using Estacionamiento.Domain.Enums;
using System.Collections.Generic;

namespace Estacionamiento.Domain.Ports
{
    public interface IParkingConfiguration
    {
        // Value objects
        public Dictionary<VehicleType, double> HourCost { get; }
        public Dictionary<VehicleType, double> DayCost { get; }
        public Dictionary<VehicleType, double> ExtraCost { get; }
        public Dictionary<VehicleType, ushort> ExtraCostCylinderCapacity { get; }
        public Dictionary<VehicleType, uint> TotalCells { get; }
        public Dictionary<System.DayOfWeek, string[]> PicoYPlaca { get; }
        public byte NumberOfHoursAsDay { get; }
        public byte NumberOfHoursPerDay { get; }
    }
}
