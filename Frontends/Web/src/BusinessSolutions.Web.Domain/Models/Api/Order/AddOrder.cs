namespace BusinessSolutions.Web.Domain.Models.Api.Order
{
    public class AddOrder
    {
        public string? Number { get; set; }
        public DateTimeOffset? Date { get; set; }
        public int ProviderId { get; set; }
    }
}
