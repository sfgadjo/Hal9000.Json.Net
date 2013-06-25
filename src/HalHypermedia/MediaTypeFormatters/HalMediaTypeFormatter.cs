using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using HalHypermedia.Converters;

namespace HalHypermedia.MediaTypeFormatters {
    public class HalMediaTypeFormatter : JsonMediaTypeFormatter {
        private readonly HalRepresentationConverter _representationConverter = new HalRepresentationConverter();
        private readonly HalLinkCollectionConverter _linkCollectionConverter = new HalLinkCollectionConverter();
        private readonly HalEmbeddedResourceCollectionConverter _embeddedResourceCollectionConverter = new HalEmbeddedResourceCollectionConverter();
        private readonly HalEmbeddedResourceRepresentationConverter _embeddedResourceRepresentationConverter = new HalEmbeddedResourceRepresentationConverter();

        public HalMediaTypeFormatter() {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/hal+json"));
            SerializerSettings.Converters.Add(_representationConverter);
            SerializerSettings.Converters.Add( _linkCollectionConverter );
            SerializerSettings.Converters.Add(_embeddedResourceCollectionConverter);
            SerializerSettings.Converters.Add( _embeddedResourceRepresentationConverter );
        }

        public override bool CanReadType(Type type) {
            return typeof (HalRepresentation).IsAssignableFrom(type);
        }

        public override bool CanWriteType(Type type) {
            return typeof( HalRepresentation ).IsAssignableFrom( type );
        }
    }
}