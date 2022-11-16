using BusinessSolutions.Services.Ordering.Domain.Requests.Item;
using BusinessSolutions.Services.Ordering.Domain.Requests.Provider;
using BusinessSolutions.Services.Ordering.Domain.Responses;

namespace BusinessSolutions.Services.Ordering.Domain.Services
{
    public interface IProviderService
    {
        Task<bool> CheckProviderAsync(GetProviderRequest request);
        Task<IEnumerable<ProviderResponse>> GetProvidersAsync();
        Task<ProviderResponse?> GetProviderAsync(GetProviderRequest request);
        Task<ProviderResponse> AddProviderAsync(AddProviderRequest request);
        Task<ProviderResponse> DeleteProviderAsync(DeleteProviderRequest request);
    }
}
