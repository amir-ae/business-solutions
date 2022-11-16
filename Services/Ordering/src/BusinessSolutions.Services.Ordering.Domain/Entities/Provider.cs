namespace BusinessSolutions.Services.Ordering.Domain.Entities
{
    public class Provider
    {
        public int ProviderId { get; set; }
        public string? ProviderName { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
