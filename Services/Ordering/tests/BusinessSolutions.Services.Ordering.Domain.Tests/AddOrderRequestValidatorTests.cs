using BusinessSolutions.Services.Ordering.Domain.Requests.Order.Validators;
using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using BusinessSolutions.Services.Ordering.Domain.Services;
using BusinessSolutions.Services.Ordering.Domain.Requests.Provider;
using BusinessSolutions.Services.Ordering.Domain.Responses;
using Moq;

namespace BusinessSolutions.Services.Ordering.Domain.Tests
{
    public class AddOrderRequestValidatorTests
    {
        private readonly Mock<IProviderService> _providerServiceMock;
        private readonly AddOrderRequestValidator _validator;
        private readonly int _providerId = int.MaxValue;

        public AddOrderRequestValidatorTests()
        {
            _providerServiceMock = new Mock<IProviderService>();
            _providerServiceMock
                .Setup(x => x.GetProviderAsync(It.IsAny<GetProviderRequest>()))
                .ReturnsAsync(() => new ProviderResponse());

            _validator = new AddOrderRequestValidator(_providerServiceMock.Object);
        }

        [Fact]
        public async Task should_have_error_when_ProviderId_is_null()
        {
            var addOrderRequest = new AddOrderRequest { Number = new Guid().ToString() };

            var result = await _validator.ValidateAsync(addOrderRequest);

            result.Errors.Select(e => e.PropertyName).ShouldContain("ProviderId");
        }

        [Fact]
        public async Task should_have_error_when_ProviderId_doesnt_exist()
        {
            _providerServiceMock
                .Setup(x => x.GetProviderAsync(It.IsAny<GetProviderRequest>()))
                .ReturnsAsync(() => null);

            var addOrderRequest = new AddOrderRequest { 
                Number = new Guid().ToString(), 
                ProviderId = _providerId 
            };

            var result = await _validator.ValidateAsync(addOrderRequest);

            result.Errors.Select(e => e.PropertyName).ShouldContain("ProviderId");
        }
    }
}
