using AutoMapper;
using Estacionamiento.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Estacionamiento.Application.Vehicle.Queries
{
    public class GetVehiclesQueryHandler : IRequestHandler<GetVehiclesQuery, IEnumerable<GetVehicleDto>>
    {
        private readonly VehicleService _vehicleService;
        private readonly IMapper _mapper;
        public GetVehiclesQueryHandler(VehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetVehicleDto>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetVehiclesAsync();

            return _mapper.Map<IEnumerable<GetVehicleDto>>(vehicles);
        }
    }
}
