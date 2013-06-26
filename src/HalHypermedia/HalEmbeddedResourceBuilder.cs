using System;
using System.Collections.Generic;

namespace HalHypermedia {
    public class HalEmbeddedResourceBuilder : IHalEmbeddedResourceBuilder {
        private readonly IResource _resource;
        private readonly HalLinkCollection _linkCollection;

        public HalEmbeddedResourceBuilder(IResource resource) {
            if (resource == null) {
                throw new ArgumentNullException("resource");
            }
            _resource = resource;
            _linkCollection = new HalLinkCollection();
        }

        public IHalEmbeddedResourceBuilder IncludeRelationWithSingleLink(HalRelation relation, HalLink link) {
            if (relation == null) {
                throw new ArgumentNullException("relation");
            }
            if (link == null) {
                throw new ArgumentNullException("link");
            }
            _linkCollection.Add(relation, link);
            return this;
        }

        public IHalEmbeddedResourceBuilder IncludeRelationWithMultipleLinks(HalRelation relation,
                                                                            IEnumerable<HalLink> links) {
            if (relation == null) {
                throw new ArgumentNullException("relation");
            }
            if (links == null) {
                throw new ArgumentNullException("links");
            }
            _linkCollection.Add(relation, links);
            return this;
        }

        public HalResource Build() {
            var result = new HalResource(_resource, _linkCollection);
            
            return result;
        }
    }
}
