using MediatR;

namespace Estacionamiento.Application.Vehicle.Queries
{
    public record HistoryOfVehicleQuery(string License) : IRequest<HistoryOfVehicleDto>;
}
