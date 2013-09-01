using System;

namespace Hal9000.Json.Net {

    /// <summary>
    /// An embedded resource.
    /// </summary>
    public sealed class HalEmbeddedResource {
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
        /// Gets the <see cref="HalDocument"/>.
        /// </summary>
        internal HalDocument Document {
            get {
                return _document;
            }
        }
    }
}
