using AutoMapper;
using BusinessSolutions.Web.Application.Common.Mapping;
using BusinessSolutions.Web.Application.Items.Commands.CreateItem;
using BusinessSolutions.Web.Application.Items.Commands.UpdateItem;
using BusinessSolutions.Web.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessSolutions.Web.Application.ViewModels;

public class ItemViewModel : IMapFrom<Item>
{
    public int ItemId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(0, (double)decimal.MaxValue)]
    public decimal Quantity { get; set; }

    [Required]
    public string? Unit { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Item, ItemViewModel>()
            .ReverseMap();

        profile.CreateMap<ItemViewModel, CreateItemCommand>();

        profile.CreateMap<ItemViewModel, UpdateItemCommand>();
    }
}
