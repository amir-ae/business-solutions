namespace BusinessSolutions.Services.Ordering.Domain.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTimeOffset Date { get; set; }
        public int ProviderId { get; set; }
        public bool IsInactive { get; set; }
        public ProviderResponse? Provider { get; set; }
        public ICollection<ItemResponse>? Items { get; set; }
    }
}
