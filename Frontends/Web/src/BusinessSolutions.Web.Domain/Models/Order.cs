namespace BusinessSolutions.Web.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTimeOffset Date { get; set; }
        public int ProviderId { get; set; }
        public bool IsInactive { get; set; }
        public Provider? Provider { get; set; }
        public List<Item>? Items { get; set; }
    }
}
