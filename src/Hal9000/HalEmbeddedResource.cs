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

namespace Hal9000.Json.Net {

    /// <summary>
    /// An embedded resource.
    /// </summary>
    public class HalEmbeddedResource {
        private readonly HalDocument _document;

        /// <summary>
        /// Creates an instance of <see cref="HalEmbeddedResource"/>.
        /// </summary>
        /// <param name="embeddedResourceBuilder">An object that builds embedded resources.</param>
        public HalEmbeddedResource(IHalEmbeddedResourceBuilder embeddedResourceBuilder) {
            if (embeddedResourceBuilder == null) {
                throw new ArgumentNullException("embeddedResourceBuilder");
            }
            _document = embeddedResourceBuilder.Build();

        }

        /// <summary>
        /// Creates an instance of <see cref="HalEmbeddedResource"/>.
        /// </summary>
        /// <remarks>This constructor is helpful for testing as a consuming project can create a wrapper object and access the internals. This does run the risk of coupling your tests to the internals of the library.</remarks>
        /// <param name="embeddedResource">The embedded resource with which to initialize this instance.</param>
        protected internal HalEmbeddedResource(HalEmbeddedResource embeddedResource)
        {
            if (embeddedResource == null)
            {
                throw new ArgumentNullException("embeddedResource");
            }

            _document = embeddedResource.Document;
        }

        /// <summary>
        /// Gets the <see cref="HalDocument"/>.
        /// </summary>
        protected internal HalDocument Document {
            get {
                return _document;
            }
        }
    }
}
