namespace BusinessSolutions.Web.Domain.Models.Api.Item
{
    public class EditItem
    {
        public int ItemId { get; set; }
        public string? Name { get; set; }
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
    }
}
