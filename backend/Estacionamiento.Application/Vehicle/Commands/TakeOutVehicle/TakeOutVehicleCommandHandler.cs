using AutoMapper;
using Estacionamiento.Domain.Exceptions;
using Estacionamiento.Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Estacionamiento.Application.Vehicle.Commands.TakeOutVehicle
{
    public class TakeOutVehicleCommandHandler : IRequestHandler<TakeOutVehicleCommand, TakeOutVehicleDto>
    {
        private readonly VehicleService _vehicleService;
        private readonly IMapper _mapper;
        public TakeOutVehicleCommandHandler(VehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        public async Task<TakeOutVehicleDto> Handle(TakeOutVehicleCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var vehicle = await _vehicleService.GetVehicleAsync(request.License);
            if (vehicle is null)
            {
                throw new NotFoundException();
            }

            var vehicleParking = await _vehicleService.TakeOutOfParkingAsync(vehicle, DateTime.Now);

            return _mapper.Map<TakeOutVehicleDto>(vehicleParking);
        }
    }
}
