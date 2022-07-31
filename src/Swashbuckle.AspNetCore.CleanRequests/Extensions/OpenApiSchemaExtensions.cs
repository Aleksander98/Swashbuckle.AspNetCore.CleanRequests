using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace Swashbuckle.AspNetCore.CleanRequests.Extensions
{
    internal static class OpenApiSchemaExtensions
    {
        internal static OpenApiSchema RemoveMatchingProperties(this OpenApiSchema schema,
            OpenApiParameter[] parameters)
        {
            foreach (var valueProperty in schema.Properties)
            {
                if (parameters.Any(parameter => parameter.Name
                    .Equals(valueProperty.Key, StringComparison.InvariantCultureIgnoreCase)))
                {
                    schema.Properties.Remove(valueProperty);
                }
            }

            return schema;
        }
    }
}
