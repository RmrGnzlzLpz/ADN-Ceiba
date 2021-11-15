using System.Collections.ObjectModel;

namespace Estacionamiento.Application.Parking.Queries
{
    public class ParkingDto
    {
        public Collection<ParkingProperty<double>> HourCost { get; set; } = new();
        public Collection<ParkingProperty<double>> DayCost { get; set; } = new();
        public Collection<ParkingProperty<double>> ExtraCost { get; set; } = new();
        public Collection<ParkingProperty<ushort>> ExtraCostCylinderCapacity { get; set; } = new();
        public Collection<ParkingProperty<uint>> TotalCells { get; set; } = new();
        public byte NumberOfHoursAsDay { get; set; }
        public byte NumberOfHoursPerDay { get; set; }
    }

    public class ParkingProperty<T> where T : struct
    {
        public string Key { get; set; } = default!;
        public T Value { get; set; } = default;
    }
}
