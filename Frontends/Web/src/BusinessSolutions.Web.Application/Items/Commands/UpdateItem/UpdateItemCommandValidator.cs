using BusinessSolutions.Web.Domain.Services;
using FluentValidation;

namespace BusinessSolutions.Web.Application.Items.Commands.UpdateItem;

public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
{
    private readonly IOrderingService service;

    public UpdateItemCommandValidator(IOrderingService service)
    {
        this.service = service;

        RuleFor(x => x.ItemId)
                .NotEmpty()
                .MustAsync(ItemExists).WithMessage("Item must exists");
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Quantity).Must(x => x > 0);
        RuleFor(x => x.Unit).NotEmpty();
    }

    private async Task<bool> ItemExists(int id, CancellationToken cancellationToken)
    {
        var item = await service.GetItemById(id);

        return item != null;
    }
}
