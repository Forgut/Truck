using Truck.Core.Entities.Trucks;

namespace Truck.Api.Model.Truck
{
    public class UpdateTruckDataRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
