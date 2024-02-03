using FluentValidation;
using Truck.Core.Application.Trucks;
using Truck.Core.Entities.Trucks;

namespace Truck.Core.Domain.Trucks
{
    public record CreateTruckParameters(string Code, string Name, string? Description);

    public interface ICreateTruck
    {
        TruckDto Create(CreateTruckParameters parameters);
    }

    internal class CreateTruck : ICreateTruck
    {
        private readonly IValidator<TruckDto> _validator;
        private readonly ITruckRepository _truckRepository;

        public CreateTruck(IValidator<TruckDto> validator, ITruckRepository truckRepository)
        {
            _validator = validator;
            _truckRepository = truckRepository;
        }

        public TruckDto Create(CreateTruckParameters parameters)
        {
            var truck = new TruckDto()
            {
                Code = parameters.Code,
                Name = parameters.Name,
                Description = parameters.Description,
                Status = ETruckStatus.OutOfService
            };

            _validator.ValidateAndThrow(truck);

            _truckRepository.AddTruck(truck);

            return truck;
        }
    }
}
