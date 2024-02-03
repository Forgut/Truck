namespace Truck.Core.Entities.Trucks
{
    public class TruckNotFoundException : Exception
    {
        public TruckNotFoundException(int id) : base($"Truck with id {id} was not found")
        {

        }
    }
}
