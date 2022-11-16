namespace BusinessSolutions.Services.Ordering.Domain.Responses
{
    public class ErrorResponse
    {
        public IDictionary<string, List<string>>? Errors { get; set; }
    }
}
