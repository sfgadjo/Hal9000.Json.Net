using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Hal9000.Json.Net.Converters {
    internal sealed class HalLinkCollectionConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

            var linkCollection = value as HalLinkCollection;
            if (linkCollection == null) {
                const string format = "The target value is not of the expected type. Expected type: {0}";
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, format,
                                                                  typeof (HalLinkCollection).Name));
            }

            writer.WriteStartObject();

            foreach (var linkPair in linkCollection) {
                writer.WritePropertyName(linkPair.Key.Value);
                serializer.Serialize(writer, linkPair.Value);
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer) {
            return reader.Value;
        }

        public override bool CanConvert(Type objectType) {
            return (typeof (HalLinkCollection)).IsAssignableFrom(objectType);
        }
    }
}