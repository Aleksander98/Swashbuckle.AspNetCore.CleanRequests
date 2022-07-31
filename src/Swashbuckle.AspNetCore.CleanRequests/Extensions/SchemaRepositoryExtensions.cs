using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Swashbuckle.AspNetCore.CleanRequests.Extensions
{
    internal static class SchemaRepositoryExtensions
    {
        internal static bool TryGetByName(this SchemaRepository schemaRepository, 
            string name, out OpenApiSchema schemas)
        {
            if (!schemaRepository.Schemas.ContainsKey(name))
            {
                schemas = null;

                return false;
            }

            schemas = schemaRepository.Schemas
                .Where(schema => schema.Key.Equals(name))
                .Select(schema => schema.Value)
                .FirstOrDefault();

            return true;
        }
    }
}
