using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Swagger;

public class GenericOperationSummaryFilter : IOperationFilter
{
    public void Apply(OpenApiOperation op, OperationFilterContext ctx)
    {
        if (ctx.ApiDescription.ActionDescriptor is not ControllerActionDescriptor cad)
            return;

        var controllerName = cad.ControllerName;
        var action = cad.ActionName.ToLowerInvariant();

        // Summary por verbo/acción
        if (action.StartsWith("getall"))
            op.Summary ??= $"Get all {controllerName}";
        else if (action.StartsWith("getbyid"))
            op.Summary ??= $"Get {controllerName} by id";
        else if (action.StartsWith("create"))
            op.Summary ??= $"Create {controllerName}";
        else if (action.StartsWith("update"))
            op.Summary ??= $"Update {controllerName}";
        else if (action.StartsWith("delete"))
            op.Summary ??= $"Delete {controllerName}";

        // Descripción genérica
        op.Description ??= $"{cad.MethodInfo} {controllerName}";
    }
}
