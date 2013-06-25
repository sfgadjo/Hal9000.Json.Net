using System;

namespace HalHypermedia {
    public class HalRepresentation {
        private readonly IHalResource _resource;
        private readonly HalLinkCollection _linkCollection;
        private readonly HalEmbeddedResourceCollection _embeddedResourceCollection;

        public HalRepresentation(IHalResource resource) {
            if (resource == null) {
                throw new ArgumentNullException("resource");
            }
            _resource = resource;
        }

        public HalRepresentation(IHalResource resource, HalLinkCollection linkCollection)
            : this(resource) {
            if (linkCollection == null) {
                throw new ArgumentNullException("linkCollection");
            }
            _linkCollection = linkCollection;
        }

        public HalRepresentation(IHalResource resource, HalLinkCollection linkCollection,
                                 HalEmbeddedResourceCollection embeddedResourceCollection)
            : this(resource, linkCollection) {
            if (embeddedResourceCollection == null) {
                throw new ArgumentNullException("embeddedResourceCollection");
            }
            _embeddedResourceCollection = embeddedResourceCollection;
        }

        public static HalRepresentation CreateWithLinks(IHalResource resource, HalLinkCollection linkCollection) {
            var result = new HalRepresentation(resource, linkCollection);
            return result;
        }

        public static HalRepresentation CreateWithLinksAndEmbedded(IHalResource resource,
                                                                   HalLinkCollection linkCollection,
                                                                   HalEmbeddedResourceCollection
                                                                       embeddedResourceCollection) {
            var result = new HalRepresentation(resource, linkCollection, embeddedResourceCollection);
            return result;
        }

        public IHalResource Resource {
            get { return _resource; }
        }

        public HalLinkCollection LinkCollection {
            get { return _linkCollection; }
        }

        public HalEmbeddedResourceCollection EmbeddedResourceCollection {
            get { return _embeddedResourceCollection; }
        }
    }
}