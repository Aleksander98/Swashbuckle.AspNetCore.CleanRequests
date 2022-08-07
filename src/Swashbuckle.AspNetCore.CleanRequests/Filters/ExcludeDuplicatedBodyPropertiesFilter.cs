using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.CleanRequests.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;

namespace Swashbuckle.AspNetCore.CleanRequests.Filters
{
    /// <summary>
    /// Operation filter to exclude body properties that are already defined for e.g. in path.
    /// </summary>
    public class ExcludeDuplicatedBodyPropertiesFilter : IOperationFilter
    {
        private readonly ParameterLocation[] _parameterLocations;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parameterLocations">Locations to search for duplicates.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ExcludeDuplicatedBodyPropertiesFilter(params ParameterLocation[] parameterLocations)
        {
            _parameterLocations = parameterLocations ?? throw new ArgumentNullException(nameof(parameterLocations));
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var parameters = operation.GetParametersWithLocation(_parameterLocations);

            if (parameters.Length <= 0) return;

            var fromBodyParameters = context.MethodInfo
                .GetParameters()
                .Where(parameter => parameter.GetCustomAttribute<FromBodyAttribute>() != null);

            foreach (var fromBodyParameter in fromBodyParameters)
            {
                if (context.SchemaRepository.TryGetByType(fromBodyParameter.ParameterType, out var schema))
                {
                    schema.RemoveMatchingProperties(parameters);
                }
            }
        }
    }
}
