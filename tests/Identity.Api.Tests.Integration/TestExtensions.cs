using Bogus;
using System.Runtime.Serialization;

namespace Identity.Api.Tests.Integration;
public static class TestExtensions
{
    public static Faker<T> WithRecord<T>(this Faker<T> faker) where T : class
    {
        faker.CustomInstantiator(_ => FormatterServices.GetUninitializedObject(typeof(T)) as T);
        return faker;
    }

}
