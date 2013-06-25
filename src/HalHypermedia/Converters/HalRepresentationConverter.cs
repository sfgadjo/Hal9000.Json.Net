using System;
using System.Globalization;
using System.Linq;
using HalHypermedia.Extensions;
using Newtonsoft.Json;

namespace HalHypermedia.Converters {
    public class HalRepresentationConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

            var representation = value as HalRepresentation;
            if (representation == null) {
                const string format = "The target value is not of the expected type. Expected type: {0}";
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, format,
                                                                  typeof (HalRepresentation).Name));
            }

            var resource = representation.Resource;
            if (resource == null) {
                throw new InvalidOperationException("The target resource to be serialized cannot be null.");
            }

            writer.WriteStartObject();
            Type type = resource.GetType();
            type.GetProperties().ToList().ForEach(s =>
                {
                    var propertyValue = s.GetValue(resource, null);
                    if (propertyValue != null) {
                        string propertyName = s.GetJsonPropertyName();
                        writer.WritePropertyName(propertyName);
                        serializer.Serialize(writer, propertyValue);
                    }
                });

            if (representation.LinkCollection != null) {
                writer.WritePropertyName(HalPropertyNames.Links);
                serializer.Serialize(writer, representation.LinkCollection);
            }

            if (representation.EmbeddedResourceCollection != null) {
                writer.WritePropertyName(HalPropertyNames.Embedded);
                serializer.Serialize(writer, representation.EmbeddedResourceCollection);
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer) {
            return reader.Value;
        }

        public override bool CanConvert(Type objectType) {
            return (typeof (HalRepresentation)).IsAssignableFrom(objectType);
        }
    }
}