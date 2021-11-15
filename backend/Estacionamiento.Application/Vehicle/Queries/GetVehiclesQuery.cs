using MediatR;
using System.Collections.Generic;

namespace Estacionamiento.Application.Vehicle.Queries
{
    public record GetVehiclesQuery() : IRequest<IEnumerable<GetVehicleDto>>;
}
