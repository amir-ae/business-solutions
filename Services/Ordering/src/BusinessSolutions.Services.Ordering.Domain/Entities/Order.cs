namespace BusinessSolutions.Services.Ordering.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool IsInactive { get; set; }
        public int ProviderId { get; set; }
        public virtual Provider? Provider { get; set; }
        public virtual ICollection<Item>? Items { get; set; }
    }
}