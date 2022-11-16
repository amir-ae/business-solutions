namespace BusinessSolutions.Web.Domain.Models.Api.Order
{
    public class GetOrdersByDate
    {
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
    }
}
