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

using System;
using Hal9000.Json.Net.Impl;

namespace Hal9000.Json.Net {
    /// <summary>
    /// A document that follows the conventions of the Hypermedia Application Language.
    /// </summary>
    public class HalDocument {
        private readonly HalEmbeddedResourceCollection _embeddedResourceCollection;
        private readonly IHalResource _resource;
        private readonly HalLinkCollection _linkCollection;

        /// <summary>
        /// Creates an instance of <see cref="HalDocument"/>.
        /// </summary>
        /// <remarks>This constructor is useful for unit testing your hypermedia. You can create a subclass and pass in the <see cref="HalDocument"/> that is created with on of the builders to get access to the internals of this class. This was done so that the inteface of this is clean and abstract as of the internals of how the hypermedia are created should not be of concern to users of the library. We still need ways that users can access the internals for testing reasons, and this design facilitates that.</remarks>
        /// <param name="document">An existing <see cref="HalDocument"/> from which to initialize this instance.</param>
        protected internal HalDocument(HalDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }

            _embeddedResourceCollection = document.embeddedResourceCollection;
            _resource = document.resource;
            _linkCollection = document.linkCollection;
        }

        /// <summary>
        /// Creates an instance of <see cref="HalDocument"/>.
        /// </summary>
        /// <param name="resource">The hypermedia aware resource.</param>
        /// <param name="linkCollection">A collection of hypermedia links.</param>
        protected internal HalDocument(IHalResource resource, HalLinkCollection linkCollection) {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }
            if (linkCollection == null)
            {
                throw new ArgumentNullException("linkCollection");
            }
            _resource = resource;
            _linkCollection = linkCollection;
        }

        /// <summary>
        /// Creates an instance of <see cref="HalDocument"/>.
        /// </summary>
        /// <param name="resource">The hypermedia aware resource.</param>
        /// <param name="linkCollection">A collection of hypermedia links.</param>
        /// <param name="embeddedResourceCollection">A collection of embedded resources.</param>
        protected internal HalDocument(IHalResource resource, HalLinkCollection linkCollection,
                             HalEmbeddedResourceCollection embeddedResourceCollection)
            : this(resource, linkCollection)
        {
            if (embeddedResourceCollection == null)
            {
                throw new ArgumentNullException("embeddedResourceCollection");
            }
            _embeddedResourceCollection = embeddedResourceCollection;
        }

        /// <summary>
        /// Gets the collection of embedded resources.
        /// </summary>
        protected internal HalEmbeddedResourceCollection embeddedResourceCollection {
            get { return _embeddedResourceCollection; }
        }

        /// <summary>
        /// Gets the hypermedia aware resource that is the root of this document.
        /// </summary>
        protected internal IHalResource resource {
            get { return _resource; }
        }

        /// <summary>
        /// Gets the collection of hypermedia links.
        /// </summary>
        protected internal HalLinkCollection linkCollection {
            get { return _linkCollection; }
        }
    }
}
