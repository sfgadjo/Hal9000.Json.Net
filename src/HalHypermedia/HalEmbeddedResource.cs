using System;

namespace HalHypermedia {
    public sealed class HalEmbeddedResource {
        private readonly HalResource _resource;

        public HalEmbeddedResource(IHalEmbeddedResourceBuilder embeddedResourceBuilder) {
            if (embeddedResourceBuilder == null) {
                throw new ArgumentNullException("embeddedResourceBuilder");
            }
            _resource = embeddedResourceBuilder.Build();

        }

        internal HalResource Resource {
            get {
                return _resource;
            }
        }
    }
}
