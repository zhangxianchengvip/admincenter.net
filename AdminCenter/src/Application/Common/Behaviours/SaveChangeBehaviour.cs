using AdminCenter.Domain.Common.Repository;
using Microsoft.Extensions.Logging;

namespace AdminCenter.Application;

public class SaveChangeBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly IApplicationDbContext _context;

    public SaveChangeBehaviour(ILogger<TRequest> logger, IApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        await _context.SaveChangesAsync(cancellationToken);
        
        return response;
    }
}
