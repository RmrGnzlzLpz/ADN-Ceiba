using System;

namespace Estacionamiento.Domain.Dtos
{
    public record ParkVehicleDto(
        DateTime EntryDate,
        string VehicleLicense
    );
}
