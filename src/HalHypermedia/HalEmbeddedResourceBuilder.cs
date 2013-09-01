using System;
using System.Collections.Generic;

namespace Hal9000.Json.Net {

    /// <summary>
    /// Builds an embedded HAL resource.
    /// </summary>
    public class HalEmbeddedResourceBuilder : IHalEmbeddedResourceBuilder {
        private readonly IHalResource _resource;
        private readonly HalLinkCollection _linkCollection;

        /// <summary>
        /// Creates an instance of <see cref="HalEmbeddedResourceBuilder"/>.
        /// </summary>
        /// <param name="resource">The target resource.</param>
        public HalEmbeddedResourceBuilder(IHalResource resource) {
            if (resource == null) {
                throw new ArgumentNullException("resource");
            }
            _resource = resource;
            _linkCollection = new HalLinkCollection();
        }

        /// <summary>
        /// Adds a relation with a single link to the embedded resource.
        /// </summary>
        /// <param name="relation">How the link is related to the resource.</param>
        /// <param name="link">A hypermedia link.</param>
        /// <returns>This <see cref="IHalEmbeddedResourceBuilder"/> instance.</returns>
        public IHalEmbeddedResourceBuilder IncludeRelationWithSingleLink ( HalRelation relation, HalLink link ) {
            if (relation == null) {
                throw new ArgumentNullException("relation");
            }
            if (link == null) {
                throw new ArgumentNullException("link");
            }
            _linkCollection.Add(relation, link);
            return this;
        }

        /// <summary>
        /// Adds a relation with multiple hypermedia links to the embedded resource.
        /// </summary>
        /// <param name="relation">How the link is related to the resource.</param>
        /// <param name="links">Hypermedia links.</param>
        /// <returns>This <see cref="IHalEmbeddedResourceBuilder"/> instance.</returns>
        public IHalEmbeddedResourceBuilder IncludeRelationWithMultipleLinks ( HalRelation relation,
                                                                            IEnumerable<HalLink> links) {
            if (relation == null) {
                throw new ArgumentNullException("relation");
            }
            if (links == null) {
                throw new ArgumentNullException("links");
            }
            _linkCollection.Add(relation, links);
            return this;
        }

        /// <summary>
        /// Builds a <see cref="HalDocument"/> based on the operations executed on this builder.
        /// </summary>
        /// <returns>A <see cref="HalDocument"/>.</returns>
        public HalDocument Build() {
            var result = new HalDocument(_resource, _linkCollection);
            
            return result;
        }
    }
}
