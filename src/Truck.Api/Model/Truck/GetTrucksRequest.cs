using Truck.Api.Model.Common;
using Truck.Core.Entities.Trucks;

namespace Truck.Api.Model.Truck
{
    public class GetTrucksRequest : SortRequest
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public ETruckStatus? Status { get; set; }    
    }
}
