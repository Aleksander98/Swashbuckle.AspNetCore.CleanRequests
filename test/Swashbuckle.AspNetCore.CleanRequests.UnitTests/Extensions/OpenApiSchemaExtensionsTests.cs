using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.CleanRequests.Extensions;

namespace Swashbuckle.AspNetCore.CleanRequests.UnitTests.Extensions;

public class OpenApiSchemaExtensionsTests
{
    [Fact]
    public void ShouldRemoveMatchingProperties()
    {
        // Arrange
        var firstParameter = new OpenApiParameter { Name = "MatchingKey" };
        var secondParameter = new OpenApiParameter { Name = "SecondMatchingKey" };
        var schema = new OpenApiSchema
        {
            Properties =
            {
                { firstParameter.Name, new OpenApiSchema() },
                { secondParameter.Name, new OpenApiSchema() },
                { "OtherKey", new OpenApiSchema() }
            }
        };

        // Act
        var result = schema.RemoveMatchingProperties(new[]
        {
            firstParameter,
            secondParameter
        });

        // Assert
        result.Properties.Keys.ShouldNotContain(firstParameter.Name);
        result.Properties.Keys.ShouldNotContain(secondParameter.Name);
        result.Properties.Keys.Count.ShouldBe(1);
    }
}