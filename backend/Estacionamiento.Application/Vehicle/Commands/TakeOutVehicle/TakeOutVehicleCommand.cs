using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Estacionamiento.Application.Vehicle.Commands.TakeOutVehicle
{
    public record TakeOutVehicleCommand(
        [Required] string License
    ) : IRequest<TakeOutVehicleDto>;
}
