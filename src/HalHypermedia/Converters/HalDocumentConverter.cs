using System;
using System.Globalization;
using System.Linq;
using Hal9000.Json.Net.Extensions;
using Newtonsoft.Json;

namespace Hal9000.Json.Net.Converters
{
    /// <summary>
    /// A <see cref="JsonConverter"/> that can convert a <see cref="HalDocument"/> to JSON.
    /// </summary>
    internal sealed class HalDocumentConverter : JsonConverter
    {
        /// <summary>
        /// Serialize the given object to JSON.
        /// </summary>
        /// <param name="writer">An object that can write JSON.</param>
        /// <param name="value">The target to serialize.</param>
        /// <param name="serializer">The serializer to use.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var halResource = value as HalDocument;
            if (halResource == null)
            {
                const string format = "The target value is not of the expected type. Expected type: {0}";
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, format,
                                                                  typeof(HalDocument).Name));
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

        /// <summary>
        /// Reads JSON from the given reader.
        /// </summary>
        /// <param name="reader">A reader from which read the JSON.</param>
        /// <param name="objectType">The target type.</param>
        /// <param name="existingValue">An existing value.</param>
        /// <param name="serializer">A serializer.</param>
        /// <returns>The value read.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }

        /// <summary>
        /// Returns true if the convert is able to convert the given type.
        /// </summary>
        /// <param name="objectType">Target type.</param>
        /// <returns>True if the convert is able to convert the given type.</returns>
        public override bool CanConvert(Type objectType)
        {
            return (typeof (HalDocument)).IsAssignableFrom(objectType);
        }
    }
}
