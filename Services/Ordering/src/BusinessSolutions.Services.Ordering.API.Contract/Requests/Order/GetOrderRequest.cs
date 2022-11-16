namespace BusinessSolutions.Services.Ordering.API.Contract.Requests.Order
{
    public class GetOrderRequest
    {
        public int Id { get; set; }
        public string? Number { get; set; }
    }
}
