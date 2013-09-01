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
