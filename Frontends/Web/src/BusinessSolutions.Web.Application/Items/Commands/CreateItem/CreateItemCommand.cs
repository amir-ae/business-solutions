using AutoMapper;
using BusinessSolutions.Web.Application.Common.Mapping;
using BusinessSolutions.Web.Domain.Models.Api.Item;
using BusinessSolutions.Web.Domain.Services;
using MediatR;

namespace BusinessSolutions.Web.Application.Items.Commands.CreateItem;

public class CreateItemCommand : IRequest<int>, IMapFrom<AddItem>
{
    public int OrderId { get; set; }
    public string? Name { get; set; }
    public decimal Quantity { get; set; }
    public string? Unit { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateItemCommand, AddItem>();
    }
}

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, int>
{
    private readonly IOrderingService service;
    private readonly IMapper mapper;

    public CreateItemCommandHandler(IOrderingService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    public async Task<int> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        var request = mapper.Map<AddItem>(command);

        var response = await service.PostItem(request);

        return response.ItemId;
    }
}
