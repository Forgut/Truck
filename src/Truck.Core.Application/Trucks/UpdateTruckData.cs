using FluentValidation;
using Truck.Core.Application.Trucks;
using Truck.Core.Entities.Trucks;

namespace Truck.Core.Domain.Trucks
{
    public record UpdateTruckDataParameters(string Code, string Name, string? Description);

    public interface IUpdateTruckData
    {
        void Update(int id, UpdateTruckDataParameters parameters);
    }

    internal class UpdateTruckData : IUpdateTruckData
    {
        private readonly ITruckRepository _truckRepository;
        private readonly IValidator<TruckDto> _validator;

        public UpdateTruckData(ITruckRepository truckRepository, IValidator<TruckDto> validator)
        {
            _truckRepository = truckRepository;
            _validator = validator;
        }

        public void Update(int id, UpdateTruckDataParameters parameters)
        {
            var truck = _truckRepository.GetTruck(id)
                ?? throw new TruckNotFoundException(id);

            truck.Code = parameters.Code;
            truck.Name = parameters.Name;
            truck.Description = parameters.Description;

            _validator.ValidateAndThrow(truck);

            _truckRepository.UpdateTruck(truck);
        }
    }
}