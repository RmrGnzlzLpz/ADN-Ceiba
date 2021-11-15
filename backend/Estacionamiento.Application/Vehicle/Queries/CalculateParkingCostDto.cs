using System;

namespace Estacionamiento.Application.Vehicle.Queries
{
    public class CalculateParkingCostDto
    {
        public int Hours { get; set; }
        public int Days { get; set; }
        public int Value { get; set; }
        public string License { get; set; } = default!;
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
    }
}
