using AutoMapper;
using BusinessSolutions.Web.Application.ViewModels;
using BusinessSolutions.Web.Application.Providers.Queries.GetProvider;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BusinessSolutions.Web.Domain.Models;

namespace BusinessSolutions.Web.UI.Components;

public class ProviderEditorViewComponent : ViewComponent
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProviderEditorViewComponent(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public ProviderWindowViewModel ViewModel { get; set; } = new();

    public async Task<IViewComponentResult> InvokeAsync(int? id, string? task)
    {
        if (id is null || id == 0 || string.IsNullOrEmpty(task))
        {
            ViewModel = _mapper.Map<ProviderWindowViewModel>(WindowViewModelFactory.Create(new Provider()));
        }
        else
        {
            var response = await _mediator.Send(new GetProviderQuery { ProviderId = (int)id });         
            if (response is not null && !string.IsNullOrEmpty(task))
            {
                switch (task.ToUpperInvariant())
                {
                    case "VIEW":
                        ViewModel = _mapper.Map<ProviderWindowViewModel>(WindowViewModelFactory.View(response));
                        break;
                    case "EDIT":
                        ViewModel = _mapper.Map<ProviderWindowViewModel>(WindowViewModelFactory.Edit(response));
                        break;
                    case "DELETE":
                        ViewModel = _mapper.Map<ProviderWindowViewModel>(WindowViewModelFactory.Delete(response));
                        break;
                };
            }
        }
        return View(ViewModel);
    }
}
