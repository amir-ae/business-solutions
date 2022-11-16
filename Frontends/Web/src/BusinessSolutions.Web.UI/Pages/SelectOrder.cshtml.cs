using AutoMapper;
using BusinessSolutions.Web.Application.Common.Models;
using BusinessSolutions.Web.Application.Items.Commands.UpdateItem;
using BusinessSolutions.Web.Application.Orders.Commands.DeleteOrder;
using BusinessSolutions.Web.Application.Orders.Commands.UpdateOrder;
using BusinessSolutions.Web.Application.Orders.Queries.GetOrders;
using BusinessSolutions.Web.Application.Providers.Queries.GetProviders;
using BusinessSolutions.Web.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessSolutions.Web.Application.ViewModels;
using BusinessSolutions.Web.Application.Common.Extensions;

namespace BusinessSolutions.Web.UI.Pages
{
    public class SelectOrderModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SelectOrderModel(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public List<Order> Orders { get; set; } = new();

        public List<Provider> Providers { get; set; } = new();

        [BindProperty]
        public FilterViewModel Filter { get; set; } = new();

        public bool AnyFilter
        {
            get => Filter.GetType().GetProperties()
                .Select(p => p.GetValue(Filter)?.ToString())
                .Any(value => !string.IsNullOrEmpty(value));
        }

        public async Task OnGetAsync()
        {
            if (!AnyFilter && TempData.ContainsKey("filter"))
            {
                GetFilter();
            }
            await GetProviders();
            await GetOrders();
        }

        public async Task<IActionResult> OnPostAsync(string? id, string? task)
        {
            TempData["id"] = id;
            TempData["task"] = task;
            TempData["modal"] = true;

            GetFilter();
            await GetOrders();

            return RedirectToPage(new { Filter });
        }

        public IActionResult OnPostFilter()
        {
            TempData["filter"] = Filter.Serialize();
            return RedirectToPage();
        }

        public IActionResult OnPostClear()
        {
            Filter = new();
            TempData.Remove("filter");
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(
            [FromForm] OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                var command = _mapper.Map<UpdateOrderCommand>(order);
                var response = await _mediator.Send(command);
                if (response != null)
                {
                    foreach (var item in order.Items!)
                    {
                        var updateItem = _mapper.Map<UpdateItemCommand>(item);
                        await _mediator.Send(updateItem);
                    }
                    TempData["message"] = $"Order \"{order.Number}\" Updated";
                }
                else
                {
                    TempData["message"] = "Error Updating Order";
                }
                return RedirectToPage();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync([FromForm] Order order)
        {
            await _mediator.Send(new DeleteOrderCommand { Number = order.Number });
            TempData["message"] = $"Order \"{order.Number}\" Deleted";
            TempData.Remove("orders");
            return RedirectToPage();
        }

        private void GetFilter()
        {
            string? filterData = TempData["filter"] as string;
            if (filterData is not null)
            {
                Filter = filterData.Deserialize(Filter) ?? new();
            }
        }

        private async Task GetProviders()
        {
            string? providerData = TempData["providers"] as string;
            if (providerData is not null)
            {
                var cached = providerData.Deserialize(Providers);
                if (cached is not null)
                {
                    Providers = cached;
                    return;
                }
            }
            var response = await _mediator.Send(new GetProvidersWithPaginationQuery { PageSize = int.MaxValue });
            if (response is not null)
            {
                Providers = response.Data.ToList();
                TempData["providers"] = Providers.Serialize();
            }
            else
            {
                TempData["message"] = "Error Retrieving Providers";
            }
        }

        private async Task GetOrders(bool cache = false)
        {
            if (cache)
            {
                string? orderData = TempData["orders"] as string;
                if (orderData is not null)
                {
                    var p = orderData.Deserialize(Orders);
                    if (p is not null)
                    {
                        Orders = p;
                        return;
                    }
                }
            }

            PaginatedList<Order> response;

            if (AnyFilter)
            {
                var query = _mapper.Map<GetOrdersByFilterQuery>(Filter);
                query.PageSize = int.MaxValue;

                response = await _mediator.Send(query);
            }
            else
            {
                var query = new GetOrdersWithPaginationQuery { PageSize = int.MaxValue };

                response = await _mediator.Send(query);
            }

            if (response is not null)
            {
                Orders = response.Data.OrderBy(x => x.Date).ToList();
                TempData["orders"] = Orders.Serialize();
            }
            else
            {
                TempData["message"] = "Error Retrieving Orders";
            }
        }
    }
}