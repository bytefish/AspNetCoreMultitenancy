using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetCoreMultitenancy.Multitenancy
{
    /// <summary>
    /// Adds the Tenant 
    /// </summary>
    public class AddTenantHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            var tenantHeaderParameter = new OpenApiParameter
            {
                Name = "X-TenantName",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            };

            operation.Parameters.Add(tenantHeaderParameter);
        }
    }
}