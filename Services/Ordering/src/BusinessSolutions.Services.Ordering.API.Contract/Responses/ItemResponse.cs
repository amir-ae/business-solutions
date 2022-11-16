namespace BusinessSolutions.Services.Ordering.API.Contract.Responses
{
    public class ItemResponse
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public string? Name { get; set; }
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
    }
}
