namespace Truck.Core.Entities.Trucks
{
    public class TruckDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public ETruckStatus Status { get; set; }
        public string? Description { get; set; }
    }
}
