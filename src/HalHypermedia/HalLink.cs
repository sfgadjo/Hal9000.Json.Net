using System;

namespace Hal9000.Json.Net {
    public class HalLink {
        private readonly string _href;

        public HalLink(string href)
        {
            _href = href;
            if (string.IsNullOrEmpty(href))
            {
                throw new ArgumentException("href cannot be null or empty", "href");
            }
        }

        public string Href
        {
            get { return _href; }
        }

        public string Title { get; set; }
        public string Profile { get; set; }
    }
}