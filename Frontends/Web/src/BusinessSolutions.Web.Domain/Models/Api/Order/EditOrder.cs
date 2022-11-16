namespace BusinessSolutions.Web.Domain.Models.Api.Order
{
    public class EditOrder
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public DateTimeOffset? Date { get; set; }
        public int ProviderId { get; set; }
        public bool IsInactive { get; set; }
    }
}
