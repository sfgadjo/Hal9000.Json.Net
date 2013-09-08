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
    /// A Hypermedia Application Language link as described here:
    /// http://stateless.co/hal_specification.html
    /// </summary>
    public class HalLink {
        private readonly string _href;

        /// <summary>
        /// Creates an instance of <see cref="HalLink"/>.
        /// </summary>
        /// <param name="href">The href of the link.</param>
        /// <exception cref="ArgumentException">Thrown if the href is not given.</exception>
        public HalLink(string href) {
            _href = href;
            if (string.IsNullOrEmpty(href)) {
                throw new ArgumentException("href cannot be null or empty", "href");
            }
        }

        /// <summary>
        /// Gets the href.
        /// </summary>
        public string Href {
            get { return _href; }
        }

        /// <summary>
        /// Gets and sets the title of the link, which can be a more detailed description
        /// than the relation offers.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets and sets the profile. A profile can be used to tie the link to a Domain Application Protocol.
        /// </summary>
        public string Profile { get; set; }

        /// <summary>
        /// Gets and sets the language of the link's href.
        /// </summary>
        public string Hreflang { get; set; }

        /// <summary>
        /// Gets and set whether this link is templated. 
        /// For instance, a templated link href could look like this 'http://mydomain.com/orders/{id}/'
        /// </summary>
        public bool? Templated { get; set; }
    }
}