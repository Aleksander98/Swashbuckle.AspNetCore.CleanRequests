using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.CleanRequests.Extensions;
using Swashbuckle.AspNetCore.CleanRequests.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swashbuckle.AspNetCore.CleanRequests.UnitTests.Extensions;

public class SwaggerGenOptionsExtensionsTests
{
    [Fact]
    public void ShouldAddExcludeDuplicatedQueryParametersFilterWithDefaultParameterLocation()
    {
        // Arrange
        var options = new SwaggerGenOptions();

        // Act
        options.ExcludeDuplicatedQueryParameters();

        // Assert
        options.OperationFilterDescriptors.ShouldContain(descriptor => 
            descriptor.Type == typeof(ExcludeDuplicatedQueryParametersFilter) &&
            descriptor.Arguments.Length == 1 &&
            ((ParameterLocation[])descriptor.Arguments[0])[0] == ParameterLocation.Path
        );
    }

    [Fact]
    public void ShouldAddExcludeDuplicatedQueryParametersFilterWithParameterLocations()
    {        
        // Arrange
        var parameterLocations = new[]
        {
            ParameterLocation.Path,
            ParameterLocation.Cookie,
            ParameterLocation.Header
        };
        var options = new SwaggerGenOptions();

        // Act
        options.ExcludeDuplicatedQueryParameters(parameterLocations);

        // Assert
        options.OperationFilterDescriptors.ShouldContain(descriptor =>
            descriptor.Type == typeof(ExcludeDuplicatedQueryParametersFilter) &&
            descriptor.Arguments.Length == 1 &&
            ((ParameterLocation[])descriptor.Arguments[0])
                .All(location => parameterLocations.Contains(location))
        );
    }
}