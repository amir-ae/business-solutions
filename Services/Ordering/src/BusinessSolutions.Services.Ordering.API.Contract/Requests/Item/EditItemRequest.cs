namespace BusinessSolutions.Services.Ordering.API.Contract.Requests.Item
{
    public class EditItemRequest
    {
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
    }
}
