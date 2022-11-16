using AutoMapper;
using BusinessSolutions.Web.Application.Common.Extensions;
using BusinessSolutions.Web.Application.Common.Mapping;
using BusinessSolutions.Web.Application.Orders.Commands.CreateOrder;
using BusinessSolutions.Web.Application.Orders.Commands.UpdateOrder;
using BusinessSolutions.Web.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessSolutions.Web.Application.ViewModels;

public class OrderViewModel : IMapFrom<Order>
{
    public string? Id { get; set; }

    [Display(Name = "Order Number")]
    public string? Number { get; set; }

    [Count(1, int.MaxValue, ErrorMessage = "Please add one or more items to order")]
    public List<ItemViewModel>? Items { get; set; }

    [Display(Name = "Provider Id")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a provider")]
    public int ProviderId { get; set; }

    public ProviderViewModel? Provider { get; set; }

    public DateTimeOffset? Date { get; set; }

    public virtual void AddItem(ItemViewModel item)
    {
        if (Items is null)
        {
            Items = new();
        }
        Items?.Add(item);
    }
    public virtual void RemoveItem(ItemViewModel item)
    {
        if (Items is null)
        {
            Items = new();
        }
        var orderItem = Items?.Find(k => k.ItemId == item.ItemId);
        if (orderItem is not null)
        {
            Items?.Remove(orderItem);
        }
    }
    public virtual void Clear() => Items?.Clear();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderViewModel>()
            .ReverseMap();

        profile.CreateMap<OrderViewModel, CreateOrderCommand>();

        profile.CreateMap<OrderViewModel, UpdateOrderCommand>();
    }
}