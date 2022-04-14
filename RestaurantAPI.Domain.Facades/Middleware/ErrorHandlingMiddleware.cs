using Microsoft.AspNetCore.Http;
using RestaurantAPI.Domain.Common.Exceptions;

namespace RestaurantAPI.Domain.Facades.Middleware;

public class ErrorHandlingMiddleware: IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFoundException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFoundException.Message);
        }
        catch (LoginException loginException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(loginException.Message);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}