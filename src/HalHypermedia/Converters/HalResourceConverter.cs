using System;
using System.Globalization;
using System.Linq;
using Hal9000.Json.Net.Extensions;
using Newtonsoft.Json;

namespace Hal9000.Json.Net.Converters
{
    internal sealed class HalResourceConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var halResource = value as HalResource;
            if (halResource == null)
            {
                const string format = "The target value is not of the expected type. Expected type: {0}";
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, format,
                                                                  typeof(HalResource).Name));
            }

            var resource = halResource.resource;
            if (resource == null)
            {
                throw new InvalidOperationException("The target resource to be serialized cannot be null.");
            }

            writer.WriteStartObject();
            Type type = resource.GetType();
            type.GetProperties().ToList().ForEach(s =>
            {
                var propertyValue = s.GetValue(resource, null);
                if (propertyValue != null)
                {
                    string propertyName = s.GetJsonPropertyName();
                    writer.WritePropertyName(propertyName);
                    serializer.Serialize(writer, propertyValue);
                }
            });

            if (halResource.linkCollection != null)
            {
                writer.WritePropertyName(HalPropertyNames.Links);
                serializer.Serialize(writer, halResource.linkCollection);
            }

            if (halResource.embeddedResourceCollection != null)
            {
                writer.WritePropertyName(HalPropertyNames.Embedded);
                serializer.Serialize(writer, halResource.embeddedResourceCollection);
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }

        public override bool CanConvert(Type objectType)
        {
            return (typeof (HalResource)).IsAssignableFrom(objectType);
        }
    }
}
