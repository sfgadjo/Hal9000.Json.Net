using System;

namespace HalHypermedia
{
    public sealed class HalResource
    {
        private readonly HalEmbeddedResourceCollection _embeddedResourceCollection;
        private readonly IResource _resource;
        private readonly HalLinkCollection _linkCollection;

        internal HalResource(IResource resource, HalLinkCollection linkCollection)
        {
            _resource = resource;
            _linkCollection = linkCollection;
        }

        internal HalResource(IResource resource, HalLinkCollection linkCollection,
                                       HalEmbeddedResourceCollection embeddedResourceCollection) : this(resource, linkCollection)
        {
            if (embeddedResourceCollection == null)
            {
                throw new ArgumentNullException("embeddedResourceCollection");
            }
            _embeddedResourceCollection = embeddedResourceCollection;
        }

        internal HalEmbeddedResourceCollection embeddedResourceCollection
        {
            get { return _embeddedResourceCollection; }
        }

        internal IResource resource
        {
            get { return _resource; }
        }

        internal HalLinkCollection linkCollection
        {
            get { return _linkCollection; }
        }
    }
}
