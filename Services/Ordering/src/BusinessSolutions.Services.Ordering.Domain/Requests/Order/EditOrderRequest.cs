namespace BusinessSolutions.Services.Ordering.Domain.Requests.Order
{
    public class EditOrderRequest
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTimeOffset? Date { get; set; }
        public int ProviderId { get; set; }
        public bool IsInactive { get; set; }
    }
}
