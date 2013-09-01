using System.Collections.Generic;

namespace Hal9000.Json.Net
{
    /// <summary>
    /// Defines the behavior for an object that builds a <see cref="HalDocument"/>.
    /// </summary>
    public interface IHalDocumentBuilder {

        /// <summary>
        /// Adds a hypermedia relation with a single link to this document.
        /// </summary>
        /// <param name="relation">How the link is related to the resource.</param>
        /// <param name="link">A hypermedia link.</param>
        /// <returns>This <see cref="IHalDocumentBuilder"/> instance.</returns>
        IHalDocumentBuilder IncludeRelationWithSingleLink(HalRelation relation, HalLink link);

        /// <summary>
        /// Adds a hypermedia relation with multiple links to this document.
        /// </summary>
        /// <param name="relation">How the link is related to the resource.</param>
        /// <param name="links">A collection of hypermedia links.</param>
        /// <returns>This <see cref="IHalDocumentBuilder"/> instance.</returns>
        IHalDocumentBuilder IncludeRelationWithMultipleLinks(HalRelation relation, IEnumerable<HalLink> links);

        /// <summary>
        /// Adds a single embedded relation with a single resource to this document.
        /// </summary>
        /// <param name="relation">How the embedded resource is related to the root resource.</param>
        /// <param name="embeddedResource">The embedded resource.</param>
        /// <returns>This <see cref="IHalDocumentBuilder"/> instance.</returns>
        IHalDocumentBuilder IncludeEmbeddedWithSingleResource(HalRelation relation,
                                                              HalEmbeddedResource embeddedResource);

        /// <summary>
        /// Adds an embedded relation with multiple resources.
        /// </summary>
        /// <param name="relation">How the embedded resource is related to the root resource.</param>
        /// <param name="embeddedResourcesresources">The embedded resources.</param>
        /// <returns>This <see cref="IHalDocumentBuilder"/> instance.</returns>
        IHalDocumentBuilder IncludeEmbeddedWithMultipleResources(HalRelation relation,
                                                                 IEnumerable<HalEmbeddedResource> embeddedResourcesresources);

        /// <summary>
        /// Builds a <see cref="HalDocument"/> based on the operations executed on this builder.
        /// </summary>
        /// <returns>A <see cref="HalDocument"/>.</returns>
        HalDocument Build();
    }
}