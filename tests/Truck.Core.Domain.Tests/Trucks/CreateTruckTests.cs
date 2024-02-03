using FluentValidation;
using NSubstitute;
using Truck.Core.Application.Trucks;
using Truck.Core.Domain.Trucks;
using Truck.Core.Entities.Trucks;
using Xunit;

namespace Truck.Core.Domain.Tests.Trucks
{
    public class CreateTruckTests
    {
        private readonly IValidator<TruckDto> _validator;
        private readonly ITruckRepository _truckRepository;
        private readonly CreateTruck _createTruck;

        public CreateTruckTests()
        {
            _validator = Substitute.For<IValidator<TruckDto>>();
            _truckRepository = Substitute.For<ITruckRepository>();
            _createTruck = new CreateTruck(_validator, _truckRepository);
        }

        [Fact]
        public void Should_throw_if_validation_fails()
        {
            _validator.WhenForAnyArgs(x => x.ValidateAndThrow(default))
                .Do(x => throw new Exception());

            var parameters = new CreateTruckParameters("some invalid data", "name", null);
            Assert.Throws<Exception>(() => _createTruck.Create(parameters));
        }

        [Fact]
        public void Should_return_truck_with_specified_data_if_validation_succeeds()
        {
            var parameters = new CreateTruckParameters("123", "name", null);


            var truck = _createTruck.Create(parameters);

            Assert.Equal(parameters.Code, truck.Code);
            Assert.Equal(parameters.Name, truck.Name);
            Assert.Equal(parameters.Description, truck.Description);
            Assert.Equal(ETruckStatus.OutOfService, truck.Status);
        }

        [Fact]
        public void Should_add_truck_to_database()
        {
            var parameters = new CreateTruckParameters("123", "name", "some desc");
            var truck = _createTruck.Create(parameters);
            _truckRepository.Received(1).AddTruck(truck);
        }
    }
}
