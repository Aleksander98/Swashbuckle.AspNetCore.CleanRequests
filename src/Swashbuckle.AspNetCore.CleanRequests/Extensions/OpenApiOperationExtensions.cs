using Microsoft.OpenApi.Models;
using System.Linq;

namespace Swashbuckle.AspNetCore.CleanRequests.Extensions
{
    internal static class OpenApiOperationExtensions
    {
        internal static OpenApiParameter[] GetParametersWithLocation(this OpenApiOperation operation,
            ParameterLocation[] parameterLocations)
        {
            return operation.Parameters
                .Where(parameter => parameterLocations.Any(location => parameter.In == location))
                .ToArray();
        }
    }
}
