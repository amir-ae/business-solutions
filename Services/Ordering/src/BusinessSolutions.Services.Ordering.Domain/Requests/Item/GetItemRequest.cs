namespace BusinessSolutions.Services.Ordering.Domain.Requests.Item
{
    public class GetItemRequest
    {
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; }
    }
}
