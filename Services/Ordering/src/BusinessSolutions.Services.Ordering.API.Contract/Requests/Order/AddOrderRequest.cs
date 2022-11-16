namespace BusinessSolutions.Services.Ordering.API.Contract.Requests.Order
{
    public class AddOrderRequest
    {
        public string? Number { get; set; }
        public DateTimeOffset? Date { get; set; }
        public int ProviderId { get; set; }
    }
}
