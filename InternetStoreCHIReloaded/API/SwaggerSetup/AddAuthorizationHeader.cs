using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SwaggerSetup
{
    public class AddAuthorizationHeader : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorize",
                In = ParameterLocation.Cookie,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "String"
                }
            });
        }
    }
}
