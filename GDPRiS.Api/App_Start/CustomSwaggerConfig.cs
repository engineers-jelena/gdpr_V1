using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Swashbuckle.Swagger;
using System.Web.Http.Description;
using GDPRiS.Api.Helpers;

namespace GDPRiS.Api
{
    // c.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();
    // c.IncludeXmlComments(string.Format(@"{0}\bin\GDPRiS.Api.XML", System.AppDomain.CurrentDomain.BaseDirectory));

    public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();

            var isAuthorized = apiDescription.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<TokenAuthorizeAttribute>().Any() || apiDescription.ActionDescriptor.GetCustomAttributes<TokenAuthorizeAttribute>().Any();

            var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();

            if (isAuthorized && !allowAnonymous)
            {
                if (operation.parameters == null)
                {
                    operation.parameters = new List<Parameter>();
                }

                operation.parameters.Add(new Parameter
                {
                    name = "Authorization",
                    @in = "header",
                    description = "access token",
                    required = true,
                    type = "string"
                });
            }
        }
    }
}
