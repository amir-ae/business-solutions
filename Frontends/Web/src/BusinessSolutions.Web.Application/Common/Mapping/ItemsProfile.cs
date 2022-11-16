using AutoMapper;
using BusinessSolutions.Services.Ordering.API.Contract.Requests.Item;
using BusinessSolutions.Web.Domain.Models.Api.Item;

namespace BusinessSolutions.Web.Application.Common.Mapping
{
    public class ItemsProfile : Profile
    {
        public ItemsProfile()
        {
            CreateMap<AddItem, AddItemRequest>();

            CreateMap<EditItem, EditItemRequest>();
        }
    }
}
