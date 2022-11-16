using AutoMapper;
using BusinessSolutions.Web.Application.ViewModels;
using BusinessSolutions.Web.Application.Orders.Queries.GetOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BusinessSolutions.Web.UI.Components;

public class OrderEditorViewComponent : ViewComponent
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrderEditorViewComponent(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public OrderWindowViewModel OrderViewModel { get; set; } = new();

    public async Task<IViewComponentResult> InvokeAsync(string? id, string? task)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(task))
        {
            OrderViewModel = _mapper.Map<OrderWindowViewModel>(WindowViewModelFactory.Create(new OrderViewModel()));
        }
        else
        {
            var response = await _mediator.Send(new GetOrderQuery { Number = id });
            if (response is not null && !string.IsNullOrEmpty(task))
            {
                var order = _mapper.Map<OrderViewModel>(response);
                switch (task.ToUpperInvariant())
                {
                    case "VIEW":
                        OrderViewModel = _mapper.Map<OrderWindowViewModel>(WindowViewModelFactory.View(order));
                        break;
                    case "EDIT":
                        OrderViewModel = _mapper.Map<OrderWindowViewModel>(WindowViewModelFactory.Edit(order));
                        break;
                    case "DELETE":
                        OrderViewModel = _mapper.Map<OrderWindowViewModel>(WindowViewModelFactory.Delete(order));
                        break;
                };
            }
        }
        return View(OrderViewModel);
    }
}
