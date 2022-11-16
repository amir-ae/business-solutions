using AutoMapper;
using BusinessSolutions.Web.Application.ViewModels;
using BusinessSolutions.Web.Application.Providers.Commands.CreateProvider;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusinessSolutions.Web.UI.Pages
{
    public class CreateProviderModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CreateProviderModel(IMediator mediator, IMapper mapper) 
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public ProviderViewModel Provider { get; set; } = new();

        public async Task<IActionResult> OnPostAsync(
            [FromForm(Name = "Provider")] ProviderViewModel target)
        {
            if (ModelState.IsValid)
            {
                var provider = _mapper.Map<CreateProviderCommand>(target);
                var result = await _mediator.Send(provider);
                if (result > 0)
                {
                    TempData["message"] = "Provider Created";
                }
                else 
                {
                    TempData["message"] = "Error Creating Provider";
                }
                return RedirectToPage();
            }
            return Page();
        }
    }
}
