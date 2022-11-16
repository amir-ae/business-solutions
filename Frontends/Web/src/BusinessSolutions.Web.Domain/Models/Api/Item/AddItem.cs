using BusinessSolutions.Services.Ordering.API.Contract.Requests.Item;

namespace BusinessSolutions.Web.Domain.Models.Api.Item
{
    public class AddItem
    {
        public int OrderId { get; set; }
        public string? Name { get; set; }
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }


    }
}
