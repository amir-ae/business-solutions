using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace BusinessSolutions.Web.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Business_Solutions Request: {Request}", request);
        return Task.FromResult(0);
    }
}
