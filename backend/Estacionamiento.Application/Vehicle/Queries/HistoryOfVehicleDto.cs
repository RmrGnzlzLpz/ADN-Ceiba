using System;
using System.Collections.ObjectModel;

namespace Estacionamiento.Application.Vehicle.Queries
{
    public class HistoryOfVehicleDto
    {
        public string License { get; set; } = default!;
        public Collection<VehicleParkingDto> VehicleHistory { get; set; } = default!;

    }
    public class VehicleParkingDto
    {
        public int Days { get; set; }
        public int Hours { get; set; }
        public double Value { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
    }
}
