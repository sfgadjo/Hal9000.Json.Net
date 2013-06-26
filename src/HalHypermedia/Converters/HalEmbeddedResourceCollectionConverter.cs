using System;
using System.Collections;
using System.Globalization;
using Newtonsoft.Json;

namespace HalHypermedia.Converters {
    internal sealed class HalEmbeddedResourceCollectionConverter : JsonConverter {
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

                bool isEnumerable = embeddedPair.Value is IEnumerable;

                if (isEnumerable)
                {
                    writer.WriteStartArray();
                }
                else
                {
                    writer.WriteStartObject();
                }

                serializer.Serialize(writer, embeddedPair.Value);

                if (isEnumerable)
                {
                    writer.WriteEndArray();
                }
                else
                {
                    writer.WriteEndObject();
                }
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