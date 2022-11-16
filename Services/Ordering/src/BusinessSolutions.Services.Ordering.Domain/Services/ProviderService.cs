using AutoMapper;
using BusinessSolutions.Services.Ordering.Domain.Entities;
using BusinessSolutions.Services.Ordering.Domain.Repositories;
using BusinessSolutions.Services.Ordering.Domain.Requests.Provider;
using BusinessSolutions.Services.Ordering.Domain.Responses;

namespace BusinessSolutions.Services.Ordering.Domain.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public ProviderService(IProviderRepository providerRepository,
            IOrderRepository orderRepository,
            IMapper mapper)
        {
            _providerRepository = providerRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<bool> CheckProviderAsync(GetProviderRequest request)
        {
            if (request?.ProviderId == null) throw new ArgumentNullException();

            return await _providerRepository.ExistsAsync(request.ProviderId);
        }

        public async Task<IEnumerable<ProviderResponse>> GetProvidersAsync()
        {
            var result = await _providerRepository.GetAsync();

            return result.Select(_mapper.Map<ProviderResponse>);
        }

        public async Task<ProviderResponse?> GetProviderAsync(GetProviderRequest request)
        {
            if (request?.ProviderId == null) throw new ArgumentNullException();

            var result = await _providerRepository.GetAsync(request.ProviderId);

            return result == null ? null : _mapper.Map<ProviderResponse>(result);
        }

        public async Task<ProviderResponse> AddProviderAsync(AddProviderRequest request)
        {
            var provider = _mapper.Map<Provider>(request);

            var result = _providerRepository.Add(provider);
            await _providerRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<ProviderResponse>(result);
        }

        public async Task<ProviderResponse> DeleteProviderAsync(DeleteProviderRequest request)
        {
            if (request?.ProviderId == null) throw new ArgumentNullException();

            var entity = await _providerRepository.GetAsync(request.ProviderId);

            if (entity == null)
            {
                throw new ArgumentException($"Entity with {request.ProviderId} is not present");
            }

            _providerRepository.Delete(entity);
            await _providerRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<ProviderResponse>(entity);
        }
    }
}