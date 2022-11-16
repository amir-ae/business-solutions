using AutoMapper;
using BusinessSolutions.Web.Application.Orders.Queries.GetOrders;
using BusinessSolutions.Web.Application.Providers.Queries.GetProviders;
using BusinessSolutions.Web.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusinessSolutions.Web.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public IndexModel(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public int ProvidersNumber { get; set; }

        public int OrdersNumber { get; set; }

        public async Task OnGetAsync()
        {
            var providers = await mediator.Send(new GetProvidersWithPaginationQuery());
            ProvidersNumber = providers.Data.Count();

            var orders = await mediator.Send(new GetOrdersWithPaginationQuery());
            OrdersNumber = orders.Data.Count();
        }
    }
}