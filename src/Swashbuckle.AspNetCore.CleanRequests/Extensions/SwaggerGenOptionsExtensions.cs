using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.CleanRequests.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swashbuckle.AspNetCore.CleanRequests.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="SwaggerGenOptions"/>.
    /// </summary>
    public static class SwaggerGenOptionsExtensions
    {
        /// <summary>
        /// Exclude query parameters that are already defined for e.g. in path.
        /// Use <paramref name="parameterLocations"/> to define where to look for duplicates.
        /// </summary>
        /// <param name="options"><see cref="SwaggerGenOptions"/>.</param>
        /// <param name="parameterLocations">
        /// Locations to search for duplicates.
        /// <see cref="ParameterLocation.Path"/> is the default.
        /// </param>
        /// <returns></returns>
        public static SwaggerGenOptions ExcludeDuplicatedQueryParameters(this SwaggerGenOptions options,
            params ParameterLocation[] parameterLocations)
        {
            if (parameterLocations.Length == 0)
            {
                parameterLocations = new[] { ParameterLocation.Path };
            }

            options.OperationFilter<ExcludeDuplicatedQueryParametersFilter>(parameterLocations);

            return options;
        }

        /// <summary>
        /// Exclude body properties that are already defined for e.g. in path.
        /// Use <paramref name="parameterLocations"/> to define where to look for duplicates.
        /// </summary>
        /// <param name="options"><see cref="SwaggerGenOptions"/>.</param>
        /// <param name="parameterLocations">
        /// Locations to search for duplicates.
        /// <see cref="ParameterLocation.Path"/> is the default.
        /// </param>
        /// <returns></returns>
        public static SwaggerGenOptions ExcludeDuplicatedBodyProperties(this SwaggerGenOptions options,
            params ParameterLocation[] parameterLocations)
        {
            if (parameterLocations.Length == 0)
            {
                parameterLocations = new[] { ParameterLocation.Path };
            }

            options.OperationFilter<ExcludeDuplicatedBodyPropertiesFilter>(parameterLocations);

            return options;
        }
    }
}
