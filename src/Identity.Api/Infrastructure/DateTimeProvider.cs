using Identity.Api.Application.Common.Interfaces;

namespace Identity.Api.Infrastructure;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
