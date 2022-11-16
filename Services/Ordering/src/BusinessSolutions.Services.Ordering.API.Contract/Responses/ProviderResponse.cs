namespace BusinessSolutions.Services.Ordering.API.Contract.Responses
{
    public class ProviderResponse
    {
        public int ProviderId { get; set; }
        public string? ProviderName { get; set; }
        public ICollection<OrderResponse>? Orders { get; set; }
    }
}
