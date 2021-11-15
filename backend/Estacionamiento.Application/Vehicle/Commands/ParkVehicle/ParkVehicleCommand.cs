using Estacionamiento.Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Estacionamiento.Application.Vehicle.Commands.ParkVehicle
{
    public record ParkVehicleCommand(
        [Required, RegularExpression(@"[a-zA-Z]{3}[0-9]{3}|[CcDdMmOoAa]{2}[0-9]{4}|[RrSs]{1}[0-9]{5}|[Tt]{1}[0-9]{4}|[a-zA-Z]{3}[0-9]{2}[a-zA-Z]{1}")] 
    string License,
        short CylinderCapacity,
        [Required] VehicleType Type
    ) : IRequest<ParkVehicleDto>;
}
