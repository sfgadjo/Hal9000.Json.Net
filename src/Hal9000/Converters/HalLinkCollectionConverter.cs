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