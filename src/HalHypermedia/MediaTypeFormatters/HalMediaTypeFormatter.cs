using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using HalHypermedia.Converters;

namespace HalHypermedia.MediaTypeFormatters {
    public class HalMediaTypeFormatter : JsonMediaTypeFormatter {
        private readonly HalResourceConverter _resourceConverter = new HalResourceConverter();
        private readonly HalLinkCollectionConverter _linkCollectionConverter = new HalLinkCollectionConverter();
        private readonly HalEmbeddedResourceCollectionConverter _embeddedResourceCollectionConverter = new HalEmbeddedResourceCollectionConverter();

        public HalMediaTypeFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/hal+json"));
            SerializerSettings.Converters.Add(_resourceConverter);
            SerializerSettings.Converters.Add( _linkCollectionConverter );
            SerializerSettings.Converters.Add(_embeddedResourceCollectionConverter);
        }

        public override bool CanReadType(Type type) {
            return typeof(HalResource).IsAssignableFrom(type);
        }

        public override bool CanWriteType(Type type) {
            return typeof( HalResource ).IsAssignableFrom( type );
        }
    }
}