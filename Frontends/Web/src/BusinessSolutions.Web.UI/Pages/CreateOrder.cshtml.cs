using AutoMapper;
using BusinessSolutions.Web.Application.ViewModels;
using BusinessSolutions.Web.Application.Common.Extensions;
using BusinessSolutions.Web.Application.Items.Commands.CreateItem;
using BusinessSolutions.Web.Application.Items.Commands.UpdateItem;
using BusinessSolutions.Web.Application.Orders.Commands.CreateOrder;
using BusinessSolutions.Web.Application.Orders.Commands.UpdateOrder;
using BusinessSolutions.Web.Application.Orders.Queries.GetOrder;
using BusinessSolutions.Web.Application.Providers.Queries.GetProviders;
using BusinessSolutions.Web.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusinessSolutions.Web.UI.Pages
{
    public class CreateOrderModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateOrderModel(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [BindProperty]
        public OrderViewModel Order { get; set; } = new();

        public List<Provider> Providers { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string? number, string? order)
        {
            if (number is not null) {
                var response = await _mediator.Send(new GetOrderQuery { Number = number });
                if (response is not null) {
                    Order = _mapper.Map<OrderViewModel>(response);
                }
            }
            else if (!string.IsNullOrEmpty(order)) {
                Order = order.Deserialize(Order) ?? new();
            }
            await GetProviders();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid) {
                if (string.IsNullOrEmpty(Order.Number)) {
                    var order = _mapper.Map<CreateOrderCommand>(Order);
                    var response = await _mediator.Send(order);
                    if (response != null) {
                        foreach (var item in Order.Items!)
                        {
                            var createItem = _mapper.Map<CreateItemCommand>(item);
                            createItem.OrderId = response.Id;
                            await _mediator.Send(createItem);
                        }
                        TempData["message"] = $"Order \"{response.Number}\" Created";
                    }
                    else {
                        TempData["message"] = "Error Creating Order";
                    }
                }
                else {
                    var order = _mapper.Map<UpdateOrderCommand>(Order);
                    var response = await _mediator.Send(order);
                    if (response != null) {
                        foreach (var item in Order.Items!)
                        {
                            if (item.ItemId == default)
                            {
                                var createItem = _mapper.Map<CreateItemCommand>(item);
                                createItem.OrderId = response.Id;
                                await _mediator.Send(createItem);
                            }
                            else
                            {
                                var updateItem = _mapper.Map<UpdateItemCommand>(item);
                                await _mediator.Send(updateItem);
                            }
                        }
                        TempData["message"] = $"Order \"{response.Number}\" Updated";
                    }
                    else {
                        TempData["message"] = "Error Updating Order";
                    }
                };
                RedirectToPage();
            }
            await GetProviders();
            return Page();
        }

        public IActionResult OnPostAddItemAsync()
        {
            return RedirectToPage("AddItem", new { order = Order.Serialize() });
        }

        public IActionResult OnPostEditItemAsync(string item)
        {
            return RedirectToPage("AddItem", new { order = Order.Serialize(), item });
        }

        public IActionResult OnPostRemoveItemAsync(string item)
        {
            var orderItem = item.Deserialize(new ItemViewModel());
            if (orderItem is not null) {
                Order.RemoveItem(orderItem);
            }
            return RedirectToPage(new { order = Order.Serialize() });
        }

        private async Task GetProviders()
        {
            string? providerData = TempData["providers"] as string;
            if (providerData is not null) {
                var cached = providerData.Deserialize(Providers);
                if (cached is not null) {
                    Providers = cached;
                    return;
                }
            }
            var response = await _mediator.Send(new GetProvidersWithPaginationQuery { PageSize = int.MaxValue });                
            if (response is not null) {
                Providers = response.Data.ToList();
                TempData["providers"] = Providers.Serialize();
            }
            else {
                TempData["message"] = "Error Retrieving Providers";
            }
        }
    }
}
