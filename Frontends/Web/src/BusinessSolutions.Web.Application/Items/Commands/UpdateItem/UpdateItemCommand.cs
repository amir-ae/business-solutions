using AutoMapper;
using BusinessSolutions.Web.Application.Common.Mapping;
using BusinessSolutions.Web.Domain.Models.Api.Item;
using BusinessSolutions.Web.Domain.Services;
using MediatR;

namespace BusinessSolutions.Web.Application.Items.Commands.UpdateItem;

public class UpdateItemCommand : IRequest<int>, IMapFrom<EditItem>
{
    public int ItemId { get; set; }
    public string? Name { get; set; }
    public decimal Quantity { get; set; }
    public string? Unit { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateItemCommand, EditItem>();
    }
}

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, int>
{
    private readonly IOrderingService service;
    private readonly IMapper mapper;

    public UpdateItemCommandHandler(IOrderingService service, IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
    }

    public async Task<int> Handle(UpdateItemCommand command, CancellationToken cancellationToken)
    {
        var request = mapper.Map<EditItem>(command);

        var response = await service.PutItem(request);

        return response.ItemId;
    }
}
