namespace BusinessSolutions.Web.Domain.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public string? Name { get; set; }
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
    }
}
