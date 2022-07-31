using Swashbuckle.AspNetCore.CleanRequests.Filters;

namespace Swashbuckle.AspNetCore.CleanRequests.UnitTests.Filters;

public class ExcludeDuplicatedBodyPropertiesFilterTests
{
    [Fact]
    public void ShouldThrowArgumentNullExceptionWhenParameterLocationsIsNull()
    {
        // Arrange, Act, Assert
        Should.Throw<ArgumentNullException>(() => new ExcludeDuplicatedBodyPropertiesFilter(null));
    }

    [Fact]
    public void ShouldNotThrowWhenParameterLocationsIsEmpty()
    {
        // Arrange, Act, Assert
        Should.NotThrow(() => new ExcludeDuplicatedBodyPropertiesFilter());
    }
}