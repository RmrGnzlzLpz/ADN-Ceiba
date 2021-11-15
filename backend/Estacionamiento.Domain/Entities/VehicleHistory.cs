using Estacionamiento.Domain.Exceptions;
using System;

namespace Estacionamiento.Domain.Entities
{
    public class VehicleHistory : EntityBase<int>
    {
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public double Value { get; private set; }
        public Vehicle? Vehicle { get; set; }

        protected VehicleHistory()
        {
        }

        public VehicleHistory(Vehicle vehicle, DateTime exitDate, int days, int hours, double value)
        {
            _ = vehicle ?? throw new ArgumentNullException(nameof(vehicle));
            Vehicle = vehicle;
            EntryDate = vehicle.EntryDate;
            ExitDate = exitDate;
            Days = days;
            Hours = hours;
            Value = value;
        }
    }
}
