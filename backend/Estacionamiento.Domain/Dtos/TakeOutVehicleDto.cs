using System;

namespace Estacionamiento.Domain.Dtos
{
    public record TakeOutVehicleDto(
        int Hours,
        int Days,
        DateTime EntryDate,
        DateTime ExitDate,
        double Value,
        string License
    );
}
