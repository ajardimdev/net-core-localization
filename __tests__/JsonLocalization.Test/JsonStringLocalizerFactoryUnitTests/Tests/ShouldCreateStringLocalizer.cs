using Shouldly;
using Xunit;

namespace JsonLocalization.Test.JsonStringLocalizerFactoryUnitTests.Tests
{
    public class ShouldCreateStringLocalizer
    {
        [Fact(DisplayName = "Method Create Should Create IStringLocalizer")]
        public void Should_create_string_localizer()
        {
            var factory = new JsonStringLocalizerFactory();
            var result = factory.Create(typeof(ShouldCreateStringLocalizer));
            result.ShouldBeOfType<JsonStringLocalizer>();
        }
    }
}