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
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Hal9000.Json.Net.Converters;
using Newtonsoft.Json;

namespace Hal9000.Json.Net.MediaTypeFormatters {

    /// <summary>
    /// A <see cref="JsonMediaTypeFormatter"/> that converts
    /// a <see cref="HalDocument"/> into JSON.
    /// </summary>
    public class HalJsonMediaTypeFormatter : JsonMediaTypeFormatter {

        /// <summary>
        /// The media type that is supported by this formatter.
        /// </summary>
        public static readonly string SupportedMediaType = "application/hal+json";

        private readonly HalDocumentConverter _resourceConverter = new HalDocumentConverter();
        private readonly HalLinkCollectionConverter _linkCollectionConverter = new HalLinkCollectionConverter();
        private readonly HalEmbeddedResourceCollectionConverter _embeddedResourceCollectionConverter = new HalEmbeddedResourceCollectionConverter();
        private readonly HalEmbeddedResourceConverter _embeddedResourceConverter = new HalEmbeddedResourceConverter();
        private readonly JsonConverter _defaultJsonConverter = new DefaultJsonConverter();

        /// <summary>
        /// Creates an instance of <see cref="HalJsonMediaTypeFormatter"/>.
        /// </summary>
        public HalJsonMediaTypeFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(SupportedMediaType));
            SerializerSettings.Converters.Add(_resourceConverter);
            SerializerSettings.Converters.Add( _linkCollectionConverter );
            SerializerSettings.Converters.Add(_embeddedResourceCollectionConverter);
            SerializerSettings.Converters.Add(_embeddedResourceConverter);
            //This may not be necessary
            SerializerSettings.Converters.Add(_defaultJsonConverter);
        }

        /// <summary>
        /// Returns true if the given type can be read by this formatter.
        /// </summary>
        /// <remarks>This current supports only the <see cref="HalDocument"/> type.</remarks>
        /// <param name="type">The target type.</param>
        /// <returns>True if the given type can be read by this formatter.</returns>
        public override bool CanReadType(Type type) {
            return typeof(HalDocument).IsAssignableFrom(type);
        }

        /// <summary>
        /// Returns true if the given type can be written by this formatter.
        /// </summary>
        /// <remarks>This current supports only the <see cref="HalDocument"/> type.</remarks>
        /// <param name="type">The target type.</param>
        /// <returns>True if the given type can be written by this formatter.</returns>
        public override bool CanWriteType(Type type) {
            return typeof( HalDocument ).IsAssignableFrom( type );
        }
    }
}