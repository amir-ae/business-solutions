namespace BusinessSolutions.Services.Ordering.API.Contract.Requests.Order
{
    public class GetOrdersByFilterRequest
    {
        public int? ProviderId { get; set; }
        public string? ProviderName { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public string? ItemName { get; set; }
        public string? ItemUnit { get; set; }
    }
}
