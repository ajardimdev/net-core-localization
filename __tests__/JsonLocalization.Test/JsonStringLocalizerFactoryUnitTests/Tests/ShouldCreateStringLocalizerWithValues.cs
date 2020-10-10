using System;
using Shouldly;
using Xunit;

namespace JsonLocalization.Test.JsonStringLocalizerFactoryUnitTests.Tests
{
    public class ShouldCreateStringLocalizerWithValues
    {
        [Fact(DisplayName = "Method Create Should Create IStringLocalizer with values by type ShouldCreateStringLocalizer")]
        public void Should_create_string_localizer()
        {
            var factory = new JsonStringLocalizerFactory();
            var result = factory.Create(typeof(ShouldCreateStringLocalizerWithValues));

            result.ShouldBeOfType<JsonStringLocalizer>();
            var value = result["key"].Value;
            value.ShouldNotBe("key");

        }
    }
}