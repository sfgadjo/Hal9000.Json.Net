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
