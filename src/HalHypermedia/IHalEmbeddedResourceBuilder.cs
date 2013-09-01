using System.Collections.Generic;

namespace Hal9000.Json.Net {

    /// <summary>
    /// Defines behavior for an object that can build a <see cref="HalEmbeddedResource"/>.
    /// </summary>
    public interface IHalEmbeddedResourceBuilder {

        /// <summary>
        /// Adds a relation with a single link to the embedded resource.
        /// </summary>
        /// <param name="relation">How the link is related to the resource.</param>
        /// <param name="link">A hypermedia link.</param>
        /// <returns>This <see cref="IHalEmbeddedResourceBuilder"/> instance.</returns>
        IHalEmbeddedResourceBuilder IncludeRelationWithSingleLink ( HalRelation relation, HalLink link );

        /// <summary>
        /// Adds a relation with multiple hypermedia links to the embedded resource.
        /// </summary>
        /// <param name="relation">How the link is related to the resource.</param>
        /// <param name="links">Hypermedia links.</param>
        /// <returns>This <see cref="IHalEmbeddedResourceBuilder"/> instance.</returns>
        IHalEmbeddedResourceBuilder IncludeRelationWithMultipleLinks ( HalRelation relation, IEnumerable<HalLink> links );

        /// <summary>
        /// Builds a <see cref="HalDocument"/> based on the operations executed on this builder.
        /// </summary>
        /// <returns>A <see cref="HalDocument"/>.</returns>
        HalDocument Build ();
    }
}
