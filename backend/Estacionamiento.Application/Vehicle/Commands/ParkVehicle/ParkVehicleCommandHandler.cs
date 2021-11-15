using AutoMapper;
using Estacionamiento.Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Estacionamiento.Application.Vehicle.Commands.ParkVehicle
{
    public class ParkVehicleCommandHandler : IRequestHandler<ParkVehicleCommand, ParkVehicleDto>
    {
        private readonly VehicleService _vehicleService;
        private readonly IMapper _mapper;
        public ParkVehicleCommandHandler(VehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        public async Task<ParkVehicleDto> Handle(ParkVehicleCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var vehicle = await _vehicleService.GetVehicleAsync(request.License) 
                ?? new Domain.Entities.Vehicle(request.License, request.CylinderCapacity, request.Type);

            await _vehicleService.ParkVehicleAsync(vehicle, DateTime.Now);

            return _mapper.Map<ParkVehicleDto>(vehicle);
        }
    }
}
