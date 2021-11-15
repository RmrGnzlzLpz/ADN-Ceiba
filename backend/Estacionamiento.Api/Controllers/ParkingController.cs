using System.Threading.Tasks;
using Estacionamiento.Application.Parking.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamiento.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {

        private readonly IMediator _Mediator;

        public ParkingController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet("Configuration")]
        public async Task<ParkingDto> TakeOutVehicle()
        {
            return await _Mediator.Send(new ParkingQuery());
        }
    }
}
