using System;
using System.Globalization;
using System.Linq;
using HalHypermedia.Extensions;
using Newtonsoft.Json;

namespace HalHypermedia.Converters {
    public sealed class HalEmbeddedResourceCollectionConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

            var embeddedResourceCollection = value as HalEmbeddedResourceCollection;

            if (embeddedResourceCollection == null) {
                const string format = "The target value is not of the expected type. Expected type: {0}";
                throw new InvalidOperationException( String.Format( CultureInfo.InvariantCulture, format,
                                                                  typeof( HalEmbeddedResourceCollection ).Name ) );
            }
           
            writer.WriteStartObject();
            foreach (var embeddedPair in embeddedResourceCollection) {
                writer.WritePropertyName( embeddedPair.Key.Value );
                writer.WriteStartArray();
                embeddedPair.Value.ToList().ForEach(s => serializer.Serialize(writer, s));
                writer.WriteEndArray();
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            return reader.Value;
        }

        public override bool CanConvert(Type objectType) {
            return (typeof (HalEmbeddedResourceCollection)).IsAssignableFrom(objectType);
        }
    }
}