using BusinessSolutions.Services.Ordering.API.Client.Base;
using BusinessSolutions.Services.Ordering.API.Client.Resources;

namespace BusinessSolutions.Services.Ordering.API.Client
{
    public class OrderingClient : IOrderingClient
    {
        public OrderingClient(HttpClient client)
        {
            Provider = new ProviderResource(new BaseClient(client, client.BaseAddress!.ToString()));

            Order = new OrderResource(new BaseClient(client, client.BaseAddress!.ToString()));

            Item = new ItemResource(new BaseClient(client, client.BaseAddress!.ToString()));
        }

        public IProviderResource Provider { get; }
        public IOrderResource Order { get; }
        public IItemResource Item { get; }
    }
}