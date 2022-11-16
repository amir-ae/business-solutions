using AutoMapper;
using BusinessSolutions.Services.Ordering.Domain.Entities;
using BusinessSolutions.Services.Ordering.Domain.Repositories;
using BusinessSolutions.Services.Ordering.Domain.Requests.Item;
using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using BusinessSolutions.Services.Ordering.Domain.Responses;

namespace BusinessSolutions.Services.Ordering.Domain.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, 
            IOrderRepository orderRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<bool> CheckItemAsync(GetItemRequest request)
        {
            if (request?.ItemId == null) throw new ArgumentNullException();

            return await _itemRepository.CheckAsync(request.ItemId);
        }

        public async Task<ItemResponse?> GetItemAsync(GetItemRequest request)
        {
            if (request?.ItemId == null) throw new ArgumentNullException();

            var result = await _itemRepository.GetAsync(request.ItemId);

            return result == null ? null : _mapper.Map<ItemResponse>(result);
        }

        public async Task<ItemResponse> AddItemAsync(AddItemRequest request)
        {
            var item = _mapper.Map<Item>(request);

            var result = _itemRepository.Add(item);
            await _itemRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<ItemResponse>(result);
        }

        public async Task<ItemResponse> EditItemAsync(EditItemRequest request)
        {
            if (request?.ItemId == null) throw new ArgumentNullException();

            var existingRecord = await _itemRepository.GetAsync(request.ItemId);

            if (existingRecord == null)
            {
                throw new ArgumentException($"Entity with {request.ItemId} is not present");
            }

            var entity = _mapper.Map<Item>(request);
            entity.OrderId = existingRecord.OrderId;
            var result = _itemRepository.Update(entity);

            await _itemRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<ItemResponse>(result);
        }

        public async Task<ItemResponse> DeleteItemAsync(DeleteItemRequest request)
        {
            if (request?.ItemId == null) throw new ArgumentNullException();

            var entity = await _itemRepository.GetAsync(request.ItemId);

            if (entity == null)
            {
                throw new ArgumentException($"Entity with {request.ItemId} is not present");
            }

            _itemRepository.Delete(entity);
            await _itemRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<ItemResponse>(entity);
        }
    }
}
