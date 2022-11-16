using AutoMapper;
using BusinessSolutions.Services.Ordering.API.Contract.Requests.Provider;
using BusinessSolutions.Web.Domain.Models.Api.Provider;

namespace BusinessSolutions.Web.Application.Common.Mapping
{
    public class ProvidersProfile : Profile
    {
        public ProvidersProfile()
        {
            CreateMap<AddProvider, AddProviderRequest>();
        }
    }
}
