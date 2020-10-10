using System;
using Shouldly;
using Xunit;

namespace JsonLocalization.Test.JsonStringLocalizerFactoryUnitTests.Tests
{
    public class ShouldCreateBaseStringLocalizerWithValues
    {
        [Fact(DisplayName = "Method Create Should Create IStringLocalizer with base values")]
        public void Should_create_string_localizer()
        {
            var factory = new JsonStringLocalizerFactory();
            var result = factory.Create("JsonLocalization.Test.Localization.base", string.Empty);

            result.ShouldBeOfType<JsonStringLocalizer>();
            var value = result["key"].Value;
            value.ShouldNotBe("key");

        }
    }
}