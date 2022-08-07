using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.CleanRequests.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swashbuckle.AspNetCore.CleanRequests.UnitTests.Extensions;

public class SchemaRepositoryExtensionsTests
{
    [Fact]
    public void ShouldReturnSchemaWithType()
    {
        // Arrange
        var schemaRepository = new SchemaRepository();

        var type = typeof(SchemaRepositoryExtensionsTests);
        schemaRepository.RegisterType(type, type.Name);
        schemaRepository.AddDefinition(type.Name, new OpenApiSchema());

        var otherType = typeof(SchemaRepositoryExtensions);
        schemaRepository.RegisterType(otherType, otherType.Name);
        schemaRepository.AddDefinition(otherType.Name, new OpenApiSchema());

        // Act
        var result = schemaRepository.TryGetByType(type, out var schema);

        // Assert
        result.ShouldBeTrue();
        schema.ShouldBe(schemaRepository.Schemas.Values.First());
    }

    [Fact]
    public void ShouldReturnNullWhenSchemaWithTypeDoesNotExist()
    {
        // Arrange
        var schemaRepository = new SchemaRepository();

        var type = typeof(SchemaRepositoryExtensionsTests);
        var otherType = typeof(SchemaRepositoryExtensions);
        schemaRepository.RegisterType(otherType, otherType.Name);
        schemaRepository.AddDefinition(otherType.Name, new OpenApiSchema());

        // Act
        var result = schemaRepository.TryGetByType(type, out var schema);

        // Assert
        result.ShouldBeFalse();
        schema.ShouldBeNull();
    }

    [Fact]
    public void ShouldReturnNullWhenSchemaWithTypeExistsWithoutTheDefinition()
    {
        // Arrange
        var schemaRepository = new SchemaRepository();

        var type = typeof(SchemaRepositoryExtensionsTests);
        schemaRepository.RegisterType(type, type.Name);

        // Act
        var result = schemaRepository.TryGetByType(type, out var schema);

        // Assert
        result.ShouldBeFalse();
        schema.ShouldBeNull();
    }
}