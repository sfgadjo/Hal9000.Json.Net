using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace Hal9000.Json.Net.Converters {
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

                //bool isEnumerable = embeddedPair.Value is IEnumerable<IResource>;

                //if (isEnumerable)
                //{
                //    writer.WriteStartArray();
                //    var resources = (IEnumerable<IResource>) embeddedPair.Value;
                //    resources.ToList().ForEach( resource => serializer.Serialize( writer, resource) );
                //    writer.WriteEndArray();
                //}
                //else
                //{
                //    writer.WriteStartObject();
                    serializer.Serialize( writer, embeddedPair.Value );
                //    writer.WriteEndObject();
                //}
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