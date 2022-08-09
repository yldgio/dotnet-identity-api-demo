# Control Flow

# Via defining and throwing Exception

using error Endpoint
see [ErrorHandling](/Docs/ErrorHandling.md#Error%Endpoint)

Create custom Exception for each domain business case and map it to message/status codes
ex:

see [DuplicateUsernameException.cs](/Identity.Api/Domain/Common/Errors/DuplicateUsernameException.cs) implementing [IDomainException](/Identity.Api/Domain/Common/Errors/IDomainException.cs) and [controller implementation](/Identity.Api/Controllers/ErrorsController.cs)
