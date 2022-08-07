using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Swashbuckle.AspNetCore.CleanRequests.Extensions
{
    internal static class SchemaRepositoryExtensions
    {
        internal static bool TryGetByType(this SchemaRepository schemaRepository,
            Type type, out OpenApiSchema schema)
        {
            // Check if that type exists in SchemaRepository
            if (schemaRepository.TryLookupByType(type, out var schemaLookup))
            {
                // Get the actual schema if exists.
                if (schemaRepository.Schemas.TryGetValue(schemaLookup.Reference.Id, out schema))
                {
                    return true;
                }
            }

            schema = null; return false;
        }
    }
}
