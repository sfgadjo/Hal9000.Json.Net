using System.Web.Http;
using Hal9000.Json.Net.MediaTypeFormatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace HalWebApiExample.App_Start {
    public static class WebApiConfig {
        public static void Register ( HttpConfiguration config ) {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {
                    id = RouteParameter.Optional
                }
            );

            setSerializerSettings(config.Formatters.JsonFormatter.SerializerSettings);

            var formatter = new HalJsonMediaTypeFormatter();
            setSerializerSettings(formatter.SerializerSettings);
            config.Formatters.Add(formatter);
        }

        private static void setSerializerSettings(JsonSerializerSettings settings) {
            settings.Converters.Add(new StringEnumConverter
                {
                    CamelCaseText = true
                });
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.DefaultValueHandling = DefaultValueHandling.Populate;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Formatting = Formatting.Indented;

            //Add additional converters for special handling of your representations.
        }
    }
}
