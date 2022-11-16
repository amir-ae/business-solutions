namespace BusinessSolutions.Services.Ordering.Domain.Requests.Order
{
    public class GetOrdersByFilterRequest
    {
        public int? ProviderId { get; set; }
        public string? ProviderName { get; set; }
        public string? OrderNumber { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public string? ItemName { get; set; }
        public string? ItemUnit { get; set; }
    }
}
