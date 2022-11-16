using AutoMapper;
using BusinessSolutions.Web.Application.Common.Mapping;
using BusinessSolutions.Web.Application.Providers.Commands.CreateProvider;
using BusinessSolutions.Web.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessSolutions.Web.Application.ViewModels;

public class ProviderViewModel : IMapFrom<Provider>
{
    public int? ProviderId { get; set; }

    [Required(ErrorMessage = "The Name field is required")]
    [Display(Name = "Provider Name")]
    public string? ProviderName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Provider, ProviderViewModel>()
            .ReverseMap();

        profile.CreateMap<ProviderViewModel, CreateProviderCommand>();
    }
}