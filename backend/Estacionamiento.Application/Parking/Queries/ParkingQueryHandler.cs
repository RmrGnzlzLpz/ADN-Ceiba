using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Estacionamiento.Domain.Services;
using MediatR;

namespace Estacionamiento.Application.Parking.Queries
{
    public class ParkingQueryHandler : IRequestHandler<ParkingQuery, ParkingDto>
    {

        private readonly VehicleService _vehicleService;
        private readonly IMapper _mapper;

        public ParkingQueryHandler(VehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        public async Task<ParkingDto> Handle(ParkingQuery request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request), "request object needed to handle this task");

            return _mapper.Map<ParkingDto>(await Task.FromResult(_vehicleService.ParkingConfiguration));
        }
    }
}
