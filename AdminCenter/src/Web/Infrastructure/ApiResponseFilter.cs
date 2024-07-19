namespace AdminCenter.Web.Infrastructure;

public class ApiResponseFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);
   
        return new ApiResponse(data: result);
    }
}
