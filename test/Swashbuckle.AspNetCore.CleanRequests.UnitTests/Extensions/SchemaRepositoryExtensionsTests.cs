using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.CleanRequests.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swashbuckle.AspNetCore.CleanRequests.UnitTests.Extensions;

public class SchemaRepositoryExtensionsTests
{
    [Fact]
    public void ShouldReturnSchemaWithKey()
    {
        // Arrange
        const string key = "Key";
        var schemaRepository = new SchemaRepository
        {
            Schemas =
            {
                { key, new OpenApiSchema() },
                { "OtherKey", new OpenApiSchema() }
            }
        };

        // Act
        var result = schemaRepository.TryGetByName(key, out var schema);

        // Assert
        result.ShouldBeTrue();
        schema.ShouldBe(schemaRepository.Schemas.Values.First());
    }

    [Fact]
    public void ShouldReturnNullWhenSchemaWithKeyDoesNotExist()
    {
        // Arrange
        const string key = "Key";
        var schemaRepository = new SchemaRepository
        {
            Schemas =
            {
                { "OtherKey", new OpenApiSchema() }
            }
        };

        // Act
        var result = schemaRepository.TryGetByName(key, out var schema);

        // Assert
        result.ShouldBeFalse();
        schema.ShouldBeNull();
    }
}