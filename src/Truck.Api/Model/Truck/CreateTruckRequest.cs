namespace Truck.Api.Model.Truck
{
    public class CreateTruckRequest
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Descirption { get; set; }
    }
}
