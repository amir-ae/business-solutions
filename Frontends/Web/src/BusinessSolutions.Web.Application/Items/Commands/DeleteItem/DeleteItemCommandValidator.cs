using BusinessSolutions.Web.Domain.Services;
using FluentValidation;

namespace BusinessSolutions.Web.Application.Items.Commands.DeleteItem;

public class RemoveItemCommandValidator : AbstractValidator<DeleteItemCommand>
{
    private readonly IOrderingService service;

    public RemoveItemCommandValidator(IOrderingService service)
    {
        this.service = service;

        RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(ItemExists).WithMessage("Item must exists");
    }

    private async Task<bool> ItemExists(int id, CancellationToken cancellationToken)
    {
        var item = await service.GetItemById(id);

        return item != null;
    }
}
