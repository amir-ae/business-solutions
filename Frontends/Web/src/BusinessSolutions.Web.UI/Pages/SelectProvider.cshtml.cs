using AutoMapper;
using BusinessSolutions.Web.Application.ViewModels;
using BusinessSolutions.Web.Application.Common.Extensions;
using BusinessSolutions.Web.Application.Providers.Commands.CreateProvider;
using BusinessSolutions.Web.Application.Providers.Commands.DeleteProvider;
using BusinessSolutions.Web.Application.Providers.Queries.GetProviders;
using BusinessSolutions.Web.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusinessSolutions.Web.UI.Pages
{
    public class SelectProvider : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SelectProvider(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public List<Provider> Providers { get; set; } = new();

        public async Task OnGetAsync(string? task)
        {
            await GetProviders();
        }

        public async Task<IActionResult> OnPostAsync(string? id, string? task)
        {
            TempData["id"] = id;
            TempData["task"] = task;
            TempData["modal"] = true;

            await GetProviders();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCreateAsync(
            [FromForm(Name = "Provider")]ProviderViewModel target)
        {
            if (ModelState.IsValid)
            {
                var provider = _mapper.Map<CreateProviderCommand>(target);
                var result = await _mediator.Send(provider);
                if (result > 0) {
                    TempData["message"] = $"Provider \"{target.ProviderName}\" Created";
                }
                else {
                    TempData["message"] = "Error Creating Provider";
                }
                return RedirectToPage();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync([FromForm]Provider provider)
        {
            await _mediator.Send(new DeleteProviderCommand { Id = provider.ProviderId });
            TempData["message"] = $"Provider \"{provider.ProviderName}\" Deleted";
            TempData.Remove("providers");
            return RedirectToPage();
        }

        private async Task GetProviders()
        {
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
    }
}