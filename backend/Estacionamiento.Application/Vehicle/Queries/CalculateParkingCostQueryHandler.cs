using AutoMapper;
using Estacionamiento.Domain.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Estacionamiento.Application.Vehicle.Queries
{
    public class CalculateParkingCostQueryHandler : IRequestHandler<CalculateParkingCostQuery, CalculateParkingCostDto>
    {
        private readonly VehicleService _vehicleService;
        private readonly IMapper _mapper;

        public CalculateParkingCostQueryHandler(VehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        public async Task<CalculateParkingCostDto> Handle(CalculateParkingCostQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.GetVehicleAsync(request.License);
            if (vehicle == null)
            {
                throw new Domain.Exceptions.NotFoundException();
            }

            if (!vehicle.IsParked)
            {
                throw new Domain.Exceptions.NotParkedException();
            }

            var response = _vehicleService.CalculateParkingCost(vehicle, System.DateTime.Now);

            return _mapper.Map<CalculateParkingCostDto>(response);
        }
    }
}
