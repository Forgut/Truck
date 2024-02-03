namespace Truck.Core.Entities.Trucks
{
    public class StatusUpdateException : Exception
    {
        public StatusUpdateException(ETruckStatus currentStatus, ETruckStatus newStatus)
            : base($"Cannot update truck status from {currentStatus} to {newStatus}")
        {

        }
    }
}
