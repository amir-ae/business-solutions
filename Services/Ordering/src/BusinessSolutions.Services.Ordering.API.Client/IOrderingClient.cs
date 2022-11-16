using BusinessSolutions.Services.Ordering.API.Client.Resources;

namespace BusinessSolutions.Services.Ordering.API.Client
{
    public interface IOrderingClient
    {
        IProviderResource Provider { get; }
        IOrderResource Order { get; }
        IItemResource Item { get; }
    }
}