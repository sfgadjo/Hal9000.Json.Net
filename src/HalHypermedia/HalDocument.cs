namespace Hal9000.Json.Net {
    /// <summary>
    /// A document that follows the conventions of the Hypermedia Application Language.
    /// </summary>
    public sealed class HalDocument {
        private readonly HalEmbeddedResourceCollection _embeddedResourceCollection;
        private readonly IHalResource _resource;
        private readonly HalLinkCollection _linkCollection;

        /// <summary>
        /// Creates an instance of <see cref="HalDocument"/>.
        /// </summary>
        /// <param name="resource">The hypermedia aware resource.</param>
        /// <param name="linkCollection">A collection of hypermedia links.</param>
        internal HalDocument(IHalResource resource, HalLinkCollection linkCollection) {
            _resource = resource;
            _linkCollection = linkCollection;
        }

        /// <summary>
        /// Creates an instance of <see cref="HalDocument"/>.
        /// </summary>
        /// <param name="resource">The hypermedia aware resource.</param>
        /// <param name="linkCollection">A collection of hypermedia links.</param>
        /// <param name="embeddedResourceCollection">A collection of embedded resources.</param>
        internal HalDocument(IHalResource resource, HalLinkCollection linkCollection,
                             HalEmbeddedResourceCollection embeddedResourceCollection)
            : this(resource, linkCollection) {
            _embeddedResourceCollection = embeddedResourceCollection;
        }

        /// <summary>
        /// Gets the collection of embedded resources.
        /// </summary>
        internal HalEmbeddedResourceCollection embeddedResourceCollection {
            get { return _embeddedResourceCollection; }
        }

        /// <summary>
        /// Gets the hypermedia aware resource that is the root of this document.
        /// </summary>
        internal IHalResource resource {
            get { return _resource; }
        }

        /// <summary>
        /// Gets the collection of hypermedia links.
        /// </summary>
        internal HalLinkCollection linkCollection {
            get { return _linkCollection; }
        }
    }
}
