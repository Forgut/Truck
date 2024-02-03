using FluentValidation;
using NSubstitute;
using Truck.Core.Application.Trucks;
using Truck.Core.Domain.Trucks;
using Truck.Core.Entities.Trucks;
using Xunit;

namespace Truck.Core.Domain.Tests.Trucks
{
    public class TruckValidatorTests
    {
        private readonly TruckValidator _validator;
        private readonly ITruckRepository _truckRepository;

        public TruckValidatorTests()
        {
            _truckRepository = Substitute.For<ITruckRepository>();
            _validator = new TruckValidator(_truckRepository);
        }

        [Fact]
        public void Should_throw_if_code_is_not_alphanumeric_code()
        {
            var truck = new TruckDto()
            {
                Code = "TE$T",
                Name = "name",
            };

            Assert.Throws<ValidationException>(() => _validator.ValidateAndThrow(truck));
        }

        [Fact]
        public void Should_throw_if_name_is_empty()
        {
            var truck = new TruckDto()
            {
                Code = "TEST",
                Name = null!,
            };

            Assert.Throws<ValidationException>(() => _validator.ValidateAndThrow(truck));
        }

        [Fact]
        public void Should_throw_if_there_is_another_truck_with_requested_code()
        {
            var code = "TEST";

            var existingTruck = new TruckDto()
            {
                Id = 1,
                Code = code,
                Name = "name",
            };

            _truckRepository.GetTruck(code)
                .Returns(existingTruck);

            var truck = new TruckDto()
            {
                Id = 2,
                Code = code,
                Name = "name",
            };

            Assert.Throws<ValidationException>(() => _validator.ValidateAndThrow(truck));
        }

        [Fact]
        public void Should_pass_if_requested_code_is_not_unique_but_code_belongs_to_truck_with_the_same_id()
        {
            var code = "TEST";

            var existingTruck = new TruckDto()
            {
                Id = 1,
                Code = code,
                Name = "old name",
            };

            _truckRepository.GetTruck(code)
                .Returns(existingTruck);

            var truck = new TruckDto()
            {
                Id = 1,
                Code = code,
                Name = "new name",
            };

            var exception = Record.Exception(() => _validator.ValidateAndThrow(truck));
            Assert.Null(exception);
        }

        [Fact]
        public void Should_pass_when_code_is_alphanumeric_and_unique_and_name_is_not_empty()
        {
            var truck = new TruckDto()
            {
                Code = "TEST",
                Name = "name"
            };

            var exception = Record.Exception(() => _validator.ValidateAndThrow(truck));
            Assert.Null(exception);
        }
    }
}
