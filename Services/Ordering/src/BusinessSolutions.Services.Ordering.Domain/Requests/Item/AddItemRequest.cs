﻿namespace BusinessSolutions.Services.Ordering.Domain.Requests.Item
{
    public class AddItemRequest
    {
        public int OrderId { get; set; }
        public string? Name { get; set; }
        public decimal Quantity { get; set; }
        public string? Unit { get; set; }
    }
}
