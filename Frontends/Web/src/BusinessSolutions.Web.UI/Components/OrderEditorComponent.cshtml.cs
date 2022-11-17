using AutoMapper;
using BusinessSolutions.Web.Application.ViewModels;
using BusinessSolutions.Web.Application.Orders.Queries.GetOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BusinessSolutions.Web.Domain.Models;

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

    public OrderWindowViewModel ViewModel { get; set; } = new();

    public async Task<IViewComponentResult> InvokeAsync(string? id, string? task)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(task))
        {
            ViewModel = _mapper.Map<OrderWindowViewModel>(WindowViewModelFactory.Create(new Order()));
        }
        else
        {
            var response = await _mediator.Send(new GetOrderQuery { Number = id });
            if (response is not null && !string.IsNullOrEmpty(task))
            {
                switch (task.ToUpperInvariant())
                {
                    case "VIEW":
                        ViewModel = _mapper.Map<OrderWindowViewModel>(WindowViewModelFactory.View(response));
                        break;
                    case "EDIT":
                        ViewModel = _mapper.Map<OrderWindowViewModel>(WindowViewModelFactory.Edit(response));
                        break;
                    case "DELETE":
                        ViewModel = _mapper.Map<OrderWindowViewModel>(WindowViewModelFactory.Delete(response));
                        break;
                };
            }
        }
        return View(ViewModel);
    }
}
