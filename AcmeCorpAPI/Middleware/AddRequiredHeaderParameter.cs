using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

 public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "X-Api-Key",
                Description = "API Key",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema() { Type = "String" },
                Required = false,
                Example = new OpenApiString("pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp")
            });
        }
    }