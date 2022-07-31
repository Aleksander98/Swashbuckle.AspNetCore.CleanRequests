using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace Swashbuckle.AspNetCore.CleanRequests.Filters
{
    /// <summary>
    /// Operation filter to exclude query parameters that are already defined for e.g. in path.
    /// </summary>
    public class ExcludeDuplicatedQueryParametersFilter : IOperationFilter
    {
        private readonly ParameterLocation[] _parameterLocations;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parameterLocations">Locations to search for duplicates.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ExcludeDuplicatedQueryParametersFilter(params ParameterLocation[] parameterLocations)
        {
            _parameterLocations = parameterLocations ?? throw new ArgumentNullException(nameof(parameterLocations));

            if (_parameterLocations.Contains(ParameterLocation.Query))
            {
                throw new ArgumentException("ParameterLocation.Query is not allowed in this context.", nameof(_parameterLocations));
            }
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext _)
        {
            var parameters = operation.Parameters
                .Where(parameter => _parameterLocations.Any(parameterLocation => parameter.In == parameterLocation))
                .ToArray();

            if (parameters.Length <= 0) return;

            var queryParameters = operation.Parameters
                .Where(parameter => parameter.In == ParameterLocation.Query)
                .ToArray();

            foreach (var queryParameter in queryParameters)
            {
                if (parameters.Any(parameter => parameter.Name.Equals(queryParameter.Name, StringComparison.InvariantCultureIgnoreCase)))
                {
                    operation.Parameters.Remove(queryParameter);
                }
            }
        }
    }
}
