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