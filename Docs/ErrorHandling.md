# global error handling

## Middleware

introduce ErrorHandlingMiddleware:

```csharp

//new class
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {

            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new { error = e.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}

//in program.cs
    app.UseMiddleware<ErrorHandlingMiddleware>();

```

## Exception Filter

introducing ErrorHandlingFilterAttribute
```csharp

//new class
public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is null)
        {
            return;
        }
        context.Result = new ObjectResult(new { error = context.Exception.Message })
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
        context.ExceptionHandled = true;
    }
}

//in program.cs
    builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());

```

## Problem Details

## Error Endpoint
