using System;

namespace Estacionamiento.Application.Vehicle.Commands.ParkVehicle
{
    public class ParkVehicleDto
    {
        public string License { get; set; } = default!;
        public DateTime EntryDate { get; set; }
    }
}
