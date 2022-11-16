namespace BusinessSolutions.Services.Ordering.Domain.Requests.Order
{
    public class AddOrderRequest
    {
        public string? Number { get; set; }
        public DateTimeOffset? Date { get; set; }
        public int ProviderId { get; set; }
    }
}
