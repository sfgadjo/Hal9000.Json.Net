using System;

namespace HalHypermedia {
    public sealed class HalEmbeddedResourceRepresentation {
        private readonly IHalResource _resource;
        private readonly HalLinkCollection _linkCollection;

        public HalEmbeddedResourceRepresentation(IHalResource resource, HalLinkCollection linkCollection) {
            if (resource == null) {
                throw new ArgumentNullException( "resource" );
            }
            if (linkCollection == null) {
                throw new ArgumentNullException("linkCollection");
            }
            _resource = resource;
            _linkCollection = linkCollection;
        }

        public IHalResource Resource {
            get {
                return _resource;
            }
        }

        public HalLinkCollection LinkCollection {
            get {
                return _linkCollection;
            }
        }
    }
}
