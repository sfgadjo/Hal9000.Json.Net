using System;

namespace HalHypermedia {
    internal sealed class HalEmbeddedResourceRepresentation {
        private readonly IResource _resource;
        private readonly HalLinkCollection _linkCollection;

        public HalEmbeddedResourceRepresentation(IResource resource, HalLinkCollection linkCollection) {
            if (resource == null) {
                throw new ArgumentNullException( "resource" );
            }
            if (linkCollection == null) {
                throw new ArgumentNullException("linkCollection");
            }
            _resource = resource;
            _linkCollection = linkCollection;
        }

        public IResource Resource {
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
