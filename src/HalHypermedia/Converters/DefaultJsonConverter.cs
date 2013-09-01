using System;
using Newtonsoft.Json;

namespace Hal9000.Json.Net.Converters {

    /// <summary>
    /// A default JSON converter for all objects that are not a <see cref="HalDocument"/>.
    /// </summary>
    internal sealed class DefaultJsonConverter : JsonConverter {

        /// <summary>
        /// Serialize the given object to JSON.
        /// </summary>
        /// <param name="writer">An object that can write JSON.</param>
        /// <param name="value">The target to serialize.</param>
        /// <param name="serializer">The serializer to use.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            serializer.Converters.Remove(this);
            serializer.Serialize(writer, value);
            serializer.Converters.Add(this);
        }

        /// <summary>
        /// Reads JSON from the given reader.
        /// </summary>
        /// <param name="reader">A reader from which read the JSON.</param>
        /// <param name="objectType">The target type.</param>
        /// <param name="existingValue">An existing value.</param>
        /// <param name="serializer">A serializer.</param>
        /// <returns>The value read.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            return reader.Value;
        }

        /// <summary>
        /// Returns true if the convert is able to convert the given type.
        /// </summary>
        /// <param name="objectType">Target type.</param>
        /// <returns>True if the convert is able to convert the given type.</returns>
        public override bool CanConvert(Type objectType) {
            return (typeof(Object)).IsAssignableFrom(objectType);
        }
    }
}
