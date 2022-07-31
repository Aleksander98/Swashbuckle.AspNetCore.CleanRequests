using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.CleanRequests.Filters;

namespace Swashbuckle.AspNetCore.CleanRequests.UnitTests.Filters;

public class ExcludeDuplicatedQueryParametersFilterTests
{
    [Fact]
    public void ShouldThrowArgumentNullExceptionWhenParameterLocationsIsNull()
    {
        // Arrange, Act, Assert
        Should.Throw<ArgumentNullException>(() => new ExcludeDuplicatedQueryParametersFilter(null));
    }

    [Fact]
    public void ShouldNotThrowWhenParameterLocationsIsEmpty()
    {
        // Arrange, Act, Assert
        Should.NotThrow(() => new ExcludeDuplicatedQueryParametersFilter());
    }

    [Fact]
    public void ShouldThrowArgumentExceptionWhenParameterLocationsContainsParameterLocationQuery()
    {
        // Arrange, Act, Assert
        Should.Throw<ArgumentException>(() => new ExcludeDuplicatedQueryParametersFilter(
            ParameterLocation.Path,
            ParameterLocation.Query,
            ParameterLocation.Cookie, 
            ParameterLocation.Header)
        );
    }

    [Theory]
    [InlineData(ParameterLocation.Header)]
    [InlineData(ParameterLocation.Cookie)]
    [InlineData(ParameterLocation.Path)]
    public void ShouldExcludeDuplicatedParametersFromQuery(ParameterLocation parameterLocation)
    {
        // Arrange
        const string duplicatedParameterName = "DuplicatedParameter";
        var operation = new OpenApiOperation
        {
            Parameters = new List<OpenApiParameter>
            {
                new()
                {
                    In = ParameterLocation.Cookie,
                    Name = "CookieParameter"
                },
                new()
                {
                    In = ParameterLocation.Header,
                    Name = "HeaderParameter"
                },
                new()
                {
                    In = ParameterLocation.Path,
                    Name = "PathParameter"
                },
                new()
                {
                    In = ParameterLocation.Query,
                    Name = "QueryParameter"
                },
                new()
                {
                    In  = parameterLocation,
                    Name = duplicatedParameterName
                },
                new()
                {
                    In = ParameterLocation.Query,
                    Name = duplicatedParameterName
                }
            }
        };
        var filter = new ExcludeDuplicatedQueryParametersFilter(parameterLocation);

        // Act
        filter.Apply(operation, null);

        // Assert
        operation.Parameters.Count.ShouldBe(5);
        operation.Parameters.ShouldNotContain(parameter =>
            parameter.Name == duplicatedParameterName &&
            parameter.In == ParameterLocation.Query
        );
    }

    [Theory]
    [InlineData(ParameterLocation.Header)]
    [InlineData(ParameterLocation.Cookie)]
    [InlineData(ParameterLocation.Path)]
    public void ShouldNotExcludeAnyParametersFromQueryWhenThereIsNoDuplicates(ParameterLocation parameterLocation)
    {
        // Arrange
        var operation = new OpenApiOperation
        {
            Parameters = new List<OpenApiParameter>
            {
                new()
                {
                    In = ParameterLocation.Cookie,
                    Name = "CookieParameter"
                },
                new()
                {
                    In = ParameterLocation.Header,
                    Name = "HeaderParameter"
                },
                new()
                {
                    In = ParameterLocation.Path,
                    Name = "PathParameter"
                },
                new()
                {
                    In = ParameterLocation.Query,
                    Name = "QueryParameter"
                },
                new()
                {
                    In = ParameterLocation.Query,
                    Name = "OtherQueryParameter"
                }
            }
        };
        var filter = new ExcludeDuplicatedQueryParametersFilter(parameterLocation);

        // Act
        filter.Apply(operation, null);

        // Assert
        operation.Parameters.Count.ShouldBe(5);
    }
}