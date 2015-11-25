using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using mapadetrabalhomobileserviceService.Models;

namespace mapadetrabalhomobileserviceService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            
            // Set default and null value handling to "Include" for Json Serializer
            config.Formatters.JsonFormatter.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
            
            Database.SetInitializer(new mapadetrabalhomobileserviceInitializer());
        }
    }

    public class mapadetrabalhomobileserviceInitializer : ClearDatabaseSchemaIfModelChanges<mapadetrabalhomobileserviceContext>
    {
        protected override void Seed(mapadetrabalhomobileserviceContext context)
        {
   
            base.Seed(context);
        }
    }
}

