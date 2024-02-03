namespace Truck.Core.Entities.Trucks
{
    public enum ETruckStatus
    {
        Loading = 0,
        ToJob = 1,
        AtJob = 2,
        Returning = 3,
        OutOfService = 99,
    }
}
