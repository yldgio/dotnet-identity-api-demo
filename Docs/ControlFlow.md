# Control Flow

## Using Exceptions

Create custom Exception for each domain business case and map it to message/status codes
ex:

see [DuplicateUsernameException.cs](/Identity.Api/Domain/Common/Exceptions/DuplicateUsernameException.cs) implementing [IDomainException](/Identity.Api/Domain/Common/Exceptions/IDomainException.cs) and [controller implementation](/Identity.Api/Controllers/ErrorsController.cs)

Load test result for exception flow
![load test result](/Docs/exception-load-test.png)

## Using Functional Patterns

Create custom structs/classes for each domain error in business case and map it to message/status codes

### Using OneOf

Using Nuget Package OneOf

see [DuplicateUsernameError.cs](/Identity.Api/Domain/Common/Errors/DuplicateUsernameError.cs) implementing [IError](/Identity.Api/Domain/Common/Errors/IError.cs) and [controller implementation](/Identity.Api/Controllers/AuthController.cs)

and implementation of [IAuthService.Register](/Identity.Api/Domain/Services/AuthService.cs)

Load test result for Result flow
![load test result](/Docs/oneof-load-test.png)
