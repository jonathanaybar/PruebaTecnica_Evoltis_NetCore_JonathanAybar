using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Swagger;

public class GenericDefaultResponsesFilter : IOperationFilter
{
    public void Apply(OpenApiOperation op, OperationFilterContext ctx)
    {
        // Si ya hay respuestas agregadas por atributos
        if (op.Responses.Count > 0) return;

        // Defaults razonables según método
        var method = ctx.ApiDescription.HttpMethod?.ToUpperInvariant();
        switch (method)
        {
            case "GET":
                op.Responses.TryAdd("200", new OpenApiResponse { Description = "OK" });
                op.Responses.TryAdd("404", new OpenApiResponse { Description = "Not Found" });
                break;
            case "POST":
                op.Responses.TryAdd("201", new OpenApiResponse { Description = "Created" });
                op.Responses.TryAdd("400", new OpenApiResponse { Description = "Bad Request" });
                break;
            case "PUT":
                op.Responses.TryAdd("200", new OpenApiResponse { Description = "OK" });
                op.Responses.TryAdd("400", new OpenApiResponse { Description = "Bad Request" });
                op.Responses.TryAdd("404", new OpenApiResponse { Description = "Not Found" });
                break;
            case "DELETE":
                op.Responses.TryAdd("200", new OpenApiResponse { Description = "OK" });
                op.Responses.TryAdd("404", new OpenApiResponse { Description = "Not Found" });
                break;
        }
    }
}
