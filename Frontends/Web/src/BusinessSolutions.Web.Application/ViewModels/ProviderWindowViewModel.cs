using AutoMapper;
using BusinessSolutions.Web.Application.Common.Mapping;
using BusinessSolutions.Web.Domain.Models;

namespace BusinessSolutions.Web.Application.ViewModels
{
    public class ProviderWindowViewModel : IMapFrom<WindowViewModel<Provider>>
    {
        public Provider Provider { get; set; } = new();
        public string Action { get; set; } = string.Empty;
        public bool ReadOnly { get; set; }
        public string Theme { get; set; } = string.Empty;
        public bool ShowId { get; set; }
        public bool ShowAction { get; set; }
        public string CancelLabel { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WindowViewModel<Provider>, ProviderWindowViewModel>()
                .ForMember(p => p.Provider, op => op.MapFrom(v => v.ModelData ?? new Provider()));
        }
    }
}
