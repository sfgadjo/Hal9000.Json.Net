using System;
using System.Globalization;
using Newtonsoft.Json;

namespace HalHypermedia.Converters {
    internal sealed class HalEmbeddedResourceConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

            var embeddedResource = value as HalEmbeddedResource;
            if (embeddedResource == null) {
                const string format = "The target value is not of the expected type. Expected type: {0}";
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, format,
                                                                  typeof (HalRepresentation).Name));
            }

            serializer.Serialize( writer, embeddedResource.Resource );
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer) {
            return reader.Value;
        }

        public override bool CanConvert(Type objectType) {
            return (typeof (HalEmbeddedResource)).IsAssignableFrom(objectType);
        }
    }
}
