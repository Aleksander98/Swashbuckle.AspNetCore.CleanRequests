using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.CleanRequests.Extensions;

namespace Swashbuckle.AspNetCore.CleanRequests.UnitTests.Extensions;

public class OpenApiOperationExtensionsTests
{
    [Theory]
    [InlineData(ParameterLocation.Header)]
    [InlineData(ParameterLocation.Cookie)]
    [InlineData(ParameterLocation.Query)]
    [InlineData(ParameterLocation.Path)]
    public void ShouldReturnOpenApiParametersWithLocation(ParameterLocation location)
    {
        // Arrange
        var operation = new OpenApiOperation
        {
            Parameters = new List<OpenApiParameter>
            {
                new() { In = ParameterLocation.Header },
                new() { In = ParameterLocation.Cookie },
                new() { In = ParameterLocation.Query },
                new() { In = ParameterLocation.Path }
            }
        };

        // Act
        var results = operation.GetParametersWithLocation(new[] { location });

        // Assert
        results.Length.ShouldBe(1);
        results[0].In.ShouldBe(location);
    }
}