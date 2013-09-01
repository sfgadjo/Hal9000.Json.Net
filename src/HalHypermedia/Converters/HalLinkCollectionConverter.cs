using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Hal9000.Json.Net.Converters {


    /// <summary>
    /// A <see cref="JsonConverter"/> that can convert a <see cref="HalLinkCollection"/> to JSON.
    /// </summary>
    internal sealed class HalLinkCollectionConverter : JsonConverter {

        /// <summary>
        /// Serialize the given object to JSON.
        /// </summary>
        /// <param name="writer">An object that can write JSON.</param>
        /// <param name="value">The target to serialize.</param>
        /// <param name="serializer">The serializer to use.</param>
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

        /// <summary>
        /// Reads JSON from the given reader.
        /// </summary>
        /// <param name="reader">A reader from which read the JSON.</param>
        /// <param name="objectType">The target type.</param>
        /// <param name="existingValue">An existing value.</param>
        /// <param name="serializer">A serializer.</param>
        /// <returns>The value read.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer) {
            return reader.Value;
        }

        /// <summary>
        /// Returns true if the convert is able to convert the given type.
        /// </summary>
        /// <param name="objectType">Target type.</param>
        /// <returns>True if the convert is able to convert the given type.</returns>
        /// 
        public override bool CanConvert(Type objectType) {
            return (typeof (HalLinkCollection)).IsAssignableFrom(objectType);
        }
    }
}