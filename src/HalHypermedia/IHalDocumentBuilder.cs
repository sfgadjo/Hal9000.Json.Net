/*
The MIT License (MIT)

Copyright (c) 2013 Trevel Beshore

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
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