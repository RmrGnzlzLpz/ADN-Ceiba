using MediatR;

namespace Estacionamiento.Application.Vehicle.Queries
{
    public record CalculateParkingCostQuery(string License) : IRequest<CalculateParkingCostDto>;
}
