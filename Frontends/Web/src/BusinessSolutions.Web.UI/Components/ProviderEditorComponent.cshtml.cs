using AutoMapper;
using BusinessSolutions.Web.Application.ViewModels;
using BusinessSolutions.Web.Application.Providers.Queries.GetProvider;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    public ProviderWindowViewModel ProviderViewModel { get; set; } = new();

    public async Task<IViewComponentResult> InvokeAsync(int? id, string? task)
    {
        if (id is null || id == 0 || string.IsNullOrEmpty(task))
        {
            ProviderViewModel = _mapper.Map<ProviderWindowViewModel>(WindowViewModelFactory.Create(new ProviderViewModel()));
        }
        else
        {
            var response = await _mediator.Send(new GetProviderQuery { ProviderId = (int)id });         
            if (response is not null && !string.IsNullOrEmpty(task))
            {
                var provider = _mapper.Map<ProviderViewModel>(response);
                switch (task.ToUpperInvariant())
                {
                    case "VIEW":
                        ProviderViewModel = _mapper.Map<ProviderWindowViewModel>(WindowViewModelFactory.View(provider));
                        break;
                    case "EDIT":
                        ProviderViewModel = _mapper.Map<ProviderWindowViewModel>(WindowViewModelFactory.Edit(provider));
                        break;
                    case "DELETE":
                        ProviderViewModel = _mapper.Map<ProviderWindowViewModel>(WindowViewModelFactory.Delete(provider));
                        break;
                };
            }
        }
        return View(ProviderViewModel);
    }
}
