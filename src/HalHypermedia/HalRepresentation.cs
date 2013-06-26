using System;

namespace HalHypermedia {
    internal class HalRepresentation {
        private readonly IResource _resource;
        private readonly HalLinkCollection _linkCollection;
        private readonly HalEmbeddedResourceCollection _embeddedResourceCollection;

        public HalRepresentation(IResource resource) {
            if (resource == null) {
                throw new ArgumentNullException("resource");
            }
            _resource = resource;
        }

        public HalRepresentation(IResource resource, HalLinkCollection linkCollection)
            : this(resource) {
            if (linkCollection == null) {
                throw new ArgumentNullException("linkCollection");
            }
            _linkCollection = linkCollection;
        }

        public HalRepresentation(IResource resource, HalLinkCollection linkCollection,
                                 HalEmbeddedResourceCollection embeddedResourceCollection)
            : this(resource, linkCollection) {
            if (embeddedResourceCollection == null) {
                throw new ArgumentNullException("embeddedResourceCollection");
            }
            _embeddedResourceCollection = embeddedResourceCollection;
        }

        public static HalRepresentation CreateWithLinks(IResource resource, HalLinkCollection linkCollection) {
            var result = new HalRepresentation(resource, linkCollection);
            return result;
        }

        public static HalRepresentation CreateWithLinksAndEmbedded(IResource resource,
                                                                   HalLinkCollection linkCollection,
                                                                   HalEmbeddedResourceCollection
                                                                       embeddedResourceCollection) {
            var result = new HalRepresentation(resource, linkCollection, embeddedResourceCollection);
            return result;
        }

        public IResource Resource {
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