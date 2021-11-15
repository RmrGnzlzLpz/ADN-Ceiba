using System.Collections.Generic;
using System.Threading.Tasks;
using Estacionamiento.Application.Vehicle.Commands.ParkVehicle;
using Estacionamiento.Application.Vehicle.Commands.TakeOutVehicle;
using Estacionamiento.Application.Vehicle.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamiento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {

        private readonly IMediator _Mediator;

        public VehiclesController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<GetVehicleDto>> Get()
        {
            return await _Mediator.Send(new GetVehiclesQuery());
        }

        [HttpPost("Exit")]
        public async Task<TakeOutVehicleDto> TakeOutVehicle(TakeOutVehicleCommand request)
        {
            return await _Mediator.Send(request);
        }

        [HttpPost("Park")]
        public async Task<ParkVehicleDto> EnterVehicle(ParkVehicleCommand request)
        {
            return await _Mediator.Send(request);
        }

        [HttpGet("History/{license}")]
        public async Task<HistoryOfVehicleDto> HistoryOfVehicle(string license)
        {
            return await _Mediator.Send(new HistoryOfVehicleQuery(license));
        }

        [HttpGet("CalculateCost/{license}")]
        public async Task<CalculateParkingCostDto> CalculateCost(string license)
        {
            return await _Mediator.Send(new CalculateParkingCostQuery(license));
        }
    }
}
