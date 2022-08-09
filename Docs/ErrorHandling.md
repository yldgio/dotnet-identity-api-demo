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

Problem is a [standardized](https://www.rfc-editor.org/rfc/rfc7807) way of describing any kind of error thrown by an API. this standard is IETF RFC 7807 (published in 2016).

The purpose of Problem is so that “API [consumers] can be informed of both the high-level error class (using the status code) and the finer-grained details of the problem”.

### RFC 7807 Spec Standard Properties

[Standard Properties](https://www.rfc-editor.org/rfc/rfc7807#section-3.1)
RFC 7807 is a simple specification. It defines a JSON format, and an associated media type, with the JSON format defining an object that has five optional members for describing a problem detail:

- **type**: A URI reference that identifies the problem type. Ideally, the URI should resolve to human-readable information describing the type, but that’s not necessary. The problem type provides information that’s more specific than the HTTP status code itself.
- **title**: A human-readable description of the problem type, meaning that it should always be the same for the same type.
- **status**: This reflects the HTTP status code and is a convenient way to make problem details self-contained. That way they can be interpreted outside of the context of the HTTP interaction in which they were provided.
- **detail**: A human-readable description of the problem instance, explaining why the problem occurred in this specific case.
instance: A URI reference that identifies the problem instance. Ideally, the URI should resolve to information describing the problem - **instance**, but that’s not necessary.

**Example**:

```json
HTTP/1.1 401 Unauthorized
Content-Type: application/problem+json; charset=utf-8
Date: Wed, 07 Aug 2019 10:10:06 GMT
{
    "type": "https://example.com/probs/cant-view-account-details",
    "title": "Not authorized to view account details",
    "status": 401,
    "detail": "Due to privacy concerns you are not allowed to view account details of others. Only users with the role administrator are allowed to do this.",
    "instance": "/account/123456/details"
}

```

The HTTP header Content-Type for a Problem response must be application/problem+json. This makes Problem easily identified by the consumer.

```
HTTP/1.1 401 Unauthorized
Content-Type: application/problem+json; charset=utf-8
Date: Wed, 07 Aug 2019 10:10:06 GMT
```
## Error Endpoint (exception handler feature)

use exception handler with custom route handler (error endpoint):

```csharp
//in Program.cs add:
    app.UseExceptionHandler(errorHandlingPath: "/error");
```

then add a controller/action handler the response, ex:

```csharp
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        //how to access Error:
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        //ControllerBase class has helper methods to support ProblemDetails creation
        //custom status code: Problem(statusCode: 400)

        return Problem(detail: exception?.Message);

    }
}
```

alternatively we can map the handler locally:

```csharp

    app.Map("/error", (HttpContext context) =>
    {
        //accessing the exception
        Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        //no dependency injection
        //the Results Problem factory allows you to pass Extensions to customize the return values
        return Results.Problem();
    });
```

