using MediatR;

namespace Estacionamiento.Application.Parking.Queries
{
    public record ParkingQuery() : IRequest<ParkingDto>;

}
