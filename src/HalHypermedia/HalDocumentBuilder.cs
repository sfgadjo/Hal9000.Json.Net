using System;
using System.Collections.Generic;

namespace Hal9000.Json.Net
{
    /// <summary>
    /// Builds a <see cref="HalDocument"/>.
    /// </summary>
    public sealed class HalDocumentBuilder : IHalDocumentBuilder
    {
        private readonly IHalResource _resource;
        private readonly HalLinkCollection _linkCollection;
        private readonly HalEmbeddedResourceCollection _embeddedResourceCollection;

        /// <summary>
        /// Creates an instance of <see cref="HalDocumentBuilder"/>.
        /// </summary>
        /// <param name="resource">The hypermedia aware resource upon which to build the HAL document.</param>
        public HalDocumentBuilder(IHalResource resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }
            _resource = resource;
            _linkCollection = new HalLinkCollection();
            _embeddedResourceCollection = new HalEmbeddedResourceCollection();
        }

        /// <summary>
        /// Adds a hypermedia relation with a single link to this document.
        /// </summary>
        /// <example>
        /// Sample JSON output:
        /// {
        ///    _links: {
        ///               "self": { "href": "http://mysite.com/" }
        ///            } 
        /// }
        /// </example>
        /// <param name="relation">How the link is related to the resource.</param>
        /// <param name="link">A hypermedia link.</param>
        /// <returns>This <see cref="IHalDocumentBuilder"/> instance.</returns>
        public IHalDocumentBuilder IncludeRelationWithSingleLink(HalRelation relation, HalLink link)
        {
            if (relation == null)
            {
                throw new ArgumentNullException("relation");
            }
            if (link == null)
            {
                throw new ArgumentNullException("link");
            }
            _linkCollection.Add(relation, link);
            return this;
        }

        /// <summary>
        /// Adds a hypermedia relation with multiple links to this document.
        /// </summary>
        /// <example>
        /// Sample JSON output:
        /// {
        ///    _links: {
        ///               "self": [
        ///                         { "href": "http://mysite.com/" },
        ///                         { "href": "http://inside.mysite.com/{id}/", "templated": true }
        ///                       ]
        ///            } 
        /// }
        /// </example>
        /// <param name="relation">How the link is related to the resource.</param>
        /// <param name="links">A collection of hypermedia links.</param>
        /// <returns>This <see cref="IHalDocumentBuilder"/> instance.</returns>
        public IHalDocumentBuilder IncludeRelationWithMultipleLinks(HalRelation relation, IEnumerable<HalLink> links)
        {
            if (relation == null)
            {
                throw new ArgumentNullException("relation");
            }
            if (links == null)
            {
                throw new ArgumentNullException("links");
            }
            _linkCollection.Add(relation, links);
            return this;
        }

        /// <summary>
        /// Adds a single embedded relation with a single resource to this document.
        /// </summary>
        /// <example>
        /// Sample JSON output:
        /// {
        ///    "_links": { "self": { "href": "http://mysite.com/" } },
        ///    _embedded: {
        ///               "order": {
        ///                           "_links": { "self": { "href": "http://mysite.com/{id}/", "templated": true },
        ///                           "orderDate": "2013-01-05T13:43:22.800000Z",
        ///                           "items": [ ... ]                        
        ///                       }
        ///               } 
        /// }
        /// </example>
        /// <param name="relation">How the embedded resource is related to the root resource.</param>
        /// <param name="embeddedResource">The embedded resource.</param>
        /// <returns>This <see cref="IHalDocumentBuilder"/> instance.</returns>
        public IHalDocumentBuilder IncludeEmbeddedWithSingleResource ( HalRelation relation, HalEmbeddedResource embeddedResource )
        {
            if (relation == null)
            {
                throw new ArgumentNullException("relation");
            }
            if (embeddedResource == null)
            {
                throw new ArgumentNullException("embeddedResource");
            }
            _embeddedResourceCollection.Add(relation, embeddedResource);
            return this;
        }

        /// <summary>
        /// Adds an embedded relation with multiple resources.
        /// </summary>
        /// <example>
        /// Sample JSON output:
        /// {
        ///    "_links": { "self": { "href": "http://mysite.com/" } },
        ///    _embedded: {
        ///               "orders": [ 
        ///                           {
        ///                             "_links": { "self": { "href": "http://mysite.com/511/" },
        ///                             "orderDate": "2013-01-05T13:43:22.33400000Z",
        ///                             "items": [ ... ]                        
        ///                           },
        ///                           {
        ///                             "_links": { "self": { "href": "http://mysite.com/512/" },
        ///                             "orderDate": "2013-01-05T13:45:22.55200000Z",
        ///                             "items": [ ... ]                        
        ///                           }
        ///                         ]
        ///               } 
        /// }
        /// </example>
        /// <param name="relation">How the embedded resource is related to the root resource.</param>
        /// <param name="embeddedResourcesresources">The embedded resources.</param>
        /// <returns>This <see cref="IHalDocumentBuilder"/> instance.</returns>
        public IHalDocumentBuilder IncludeEmbeddedWithMultipleResources ( HalRelation relation, IEnumerable<HalEmbeddedResource> embeddedResourcesresources )
        {
            if (relation == null)
            {
                throw new ArgumentNullException("relation");
            }
            if (embeddedResourcesresources == null)
            {
                throw new ArgumentNullException("embeddedResourcesresources");
            }
            _embeddedResourceCollection.Add(relation, embeddedResourcesresources);
            return this;
        }

        /// <summary>
        /// Builds a <see cref="HalDocument"/> based on the operations executed on this builder.
        /// </summary>
        /// <returns>A <see cref="HalDocument"/>.</returns>
        public HalDocument Build()
        {
            HalDocument result;
            if (_embeddedResourceCollection.Count > 0)
            {
                result = new HalDocument(_resource, _linkCollection, _embeddedResourceCollection);
            }
            else
            {
                result = new HalDocument(_resource, _linkCollection);
            }
            return result;
        }
    }
}
