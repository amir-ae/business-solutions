﻿using AutoMapper;
using BusinessSolutions.Web.Application.Common.Mapping;
using BusinessSolutions.Web.Application.Providers.Queries.GetProviders;
using BusinessSolutions.Web.Domain.Models;
using MediatR;

namespace BusinessSolutions.Web.Application.ViewModels
{
    public class OrderWindowViewModel : IMapFrom<WindowViewModel<OrderViewModel>>
    {
        public OrderViewModel Order { get; set; } = new();
        public List<Provider> Providers { get; set; } = new();
        public string Action { get; set; } = string.Empty;
        public bool ReadOnly { get; set; }
        public string Theme { get; set; } = string.Empty;
        public bool ShowId { get; set; }
        public bool ShowAction { get; set; }
        public string CancelLabel { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WindowViewModel<OrderViewModel>, OrderWindowViewModel>()
                .ForMember(p => p.Order, op => op.MapFrom(v => v.ModelData ?? new OrderViewModel()))
                .ForMember(p => p.Providers, op => op.MapFrom<ProvidersResolver>());
        }

        public class ProvidersResolver : IValueResolver<WindowViewModel<OrderViewModel>, OrderWindowViewModel, List<Provider>>
        {
            private readonly IMediator mediator;

            public ProvidersResolver(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public List<Provider> Resolve(WindowViewModel<OrderViewModel> source, OrderWindowViewModel destination, 
                List<Provider> destMember, ResolutionContext context)
            {
                return mediator.Send(new GetProvidersWithPaginationQuery { PageSize = int.MaxValue })
                    .GetAwaiter().GetResult().Data.ToList();
            }
        }
    }
}