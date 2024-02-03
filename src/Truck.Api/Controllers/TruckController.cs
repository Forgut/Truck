using Microsoft.AspNetCore.Mvc;
using Truck.Api.Model.Truck;
using Truck.Core.Domain.Trucks;
using Truck.Core.Entities.Trucks;

namespace Truck.Api.Controllers
{
    [ApiController]
    [Route("/api/truck")]
    public class TruckController : ControllerBase
    {
        private readonly ICreateTruck _createTruck;
        private readonly IUpdateTruckStatus _updateTruckStatus;
        private readonly IUpdateTruckData _updateTruckData;
        private readonly IDeleteTruck _deleteTruck;
        private readonly IGetTruck _getTruck;
        private readonly IGetTrucks _getTrucks;

        public TruckController(ICreateTruck createTruck,
            IUpdateTruckData updateTruckData,
            IUpdateTruckStatus updateTruckStatus,
            IDeleteTruck deleteTruck,
            IGetTruck getTruck,
            IGetTrucks getTrucks)
        {
            _createTruck = createTruck;
            _updateTruckData = updateTruckData;
            _updateTruckStatus = updateTruckStatus;
            _deleteTruck = deleteTruck;
            _getTruck = getTruck;
            _getTrucks = getTrucks;
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var truck = _getTruck.Get(id);
            return Ok(truck);
        }

        [HttpGet()]
        public IActionResult Get([FromQuery] GetTrucksRequest request)
        {
            var parameters = new GetTrucksParameters(request.Code,
                                                     request.Name,
                                                     request.Status,
                                                     request);

            var trucks = _getTrucks.Get(parameters);
            return Ok(trucks);
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] CreateTruckRequest request)
        {
            var parameters = new CreateTruckParameters(request.Code,
                                                       request.Name,
                                                       request.Descirption);
            var result = _createTruck.Create(parameters);
            return Ok(result.Id);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateTruckDataRequest request)
        {
            var updateParameters = new UpdateTruckDataParameters(request.Code,
                                                             request.Name,
                                                             request.Description);
            _updateTruckData.Update(id, updateParameters);
            return NoContent();
        }

        [HttpPut("{id}/{status}")]
        public IActionResult UpdateStatus([FromRoute] int id, [FromRoute] ETruckStatus status)
        {
            _updateTruckStatus.Update(id, status);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _deleteTruck.Delete(id);
            return NoContent();
        }
    }
}
