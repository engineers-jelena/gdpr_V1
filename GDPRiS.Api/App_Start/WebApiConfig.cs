using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Newtonsoft.Json;
using GDPRiS.Api.Helpers;

namespace GDPRiS.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var json = config.Formatters.JsonFormatter;

            // Solve reference loop problem
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Use camel case for json serialization
            json.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            // Serialize enums as strings
            json.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            json.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            // Remove xml formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //Configures AutoMapper
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperConfigurationProfile>());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
