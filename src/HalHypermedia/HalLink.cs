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