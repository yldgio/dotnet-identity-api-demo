using Identity.Api.Domain.Common.Interfaces;

namespace Identity.Api.Infrastructure;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
