/*
The MIT License (MIT)

Copyright (c) 2013 Trevel Beshore

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Hal9000.Json.Net.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
                    if (!ignoreProperty(s))
                    {
                        string propertyName = s.GetJsonPropertyName(serializer.ContractResolver);
                        writer.WritePropertyName(propertyName);
                        serializer.Serialize(writer, propertyValue);
                    }
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

        private bool ignoreProperty(PropertyInfo info)
        {
            bool ignore = false;
            foreach (var attr in info.CustomAttributes)
            {
                if (attr.AttributeType.FullName == typeof(JsonIgnoreAttribute).FullName)
                {
                    ignore = true;
                }
            }
            return ignore;
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
