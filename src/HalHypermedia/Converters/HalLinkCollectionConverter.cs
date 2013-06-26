using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using HalHypermedia.Extensions;
using Newtonsoft.Json;

namespace HalHypermedia.Converters {
    internal sealed class HalLinkCollectionConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

            var linkCollection = value as HalLinkCollection;
            if (linkCollection == null) {
                const string format = "The target value is not of the expected type. Expected type: {0}";
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, format,
                                                                  typeof (HalLinkCollection).Name));
            }

            writer.WriteStartObject();
            foreach (var linkPair in linkCollection)
            {

                writer.WritePropertyName(linkPair.Key.Value);
                Type type = linkPair.Value.GetType();

                writer.WriteStartObject();
                type.GetProperties().ToList().ForEach(s =>
                    {
                        var propertyValue = s.GetValue(linkPair.Value, null);
                        if (propertyValue != null)
                        {
                            string propertyName = s.GetJsonPropertyName();
                            writer.WritePropertyName(propertyName);

                            bool isEnumerable = linkPair.Value is IEnumerable;

                            if (isEnumerable)
                            {
                                writer.WriteStartArray();
                            }
                            else
                            {
                                writer.WriteStartObject();
                            }
                            
                            serializer.Serialize(writer, propertyValue);

                            if (isEnumerable)
                            {
                                writer.WriteEndArray();
                            }
                            else
                            {
                                writer.WriteEndObject();
                            }
                        }
                    });
                
                writer.WriteEndObject();
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