using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Estacionamiento.Application.Vehicle.Queries;
using Estacionamiento.Domain.Services;
using MediatR;

namespace Estacionamiento.Application.Parking.Queries
{
    public class HistoryOfVehicleQueryHandler : IRequestHandler<HistoryOfVehicleQuery, HistoryOfVehicleDto>
    {

        private readonly VehicleService _vehicleService;
        private readonly IMapper _mapper;

        public HistoryOfVehicleQueryHandler(VehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        public async Task<HistoryOfVehicleDto> Handle(HistoryOfVehicleQuery request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request), "request object needed to handle this task");

            var vehicle = await _vehicleService.GetVehicleWithHistoryAsync(request.License);

            if (vehicle?.VehicleHistory == null)
            {
                throw new Domain.Exceptions.NotFoundException();
            }

            return _mapper.Map<HistoryOfVehicleDto>(vehicle);
        }
    }
}
