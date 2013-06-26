using System;
using System.Collections.Generic;

namespace Hal9000.Json.Net
{
    public sealed class HalResourceBuilder : IHalResourceBuilder
    {
        private readonly IResource _resource;
        private readonly HalLinkCollection _linkCollection;
        private readonly HalEmbeddedResourceCollection _embeddedResourceCollection;

        public HalResourceBuilder(IResource resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }
            _resource = resource;
            _linkCollection = new HalLinkCollection();
            _embeddedResourceCollection = new HalEmbeddedResourceCollection();
        }

        public IHalResourceBuilder IncludeRelationWithSingleLink(HalRelation relation, HalLink link)
        {
            if (relation == null)
            {
                throw new ArgumentNullException("relation");
            }
            if (link == null)
            {
                throw new ArgumentNullException("link");
            }
            _linkCollection.Add(relation, link);
            return this;
        }

        public IHalResourceBuilder IncludeRelationWithMultipleLinks(HalRelation relation, IEnumerable<HalLink> links)
        {
            if (relation == null)
            {
                throw new ArgumentNullException("relation");
            }
            if (links == null)
            {
                throw new ArgumentNullException("links");
            }
            _linkCollection.Add(relation, links);
            return this;
        }

        public IHalResourceBuilder IncludeEmbeddedWithSingleResource(HalRelation relation, HalEmbeddedResource resource)
        {
            if (relation == null)
            {
                throw new ArgumentNullException("relation");
            }
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }
            _embeddedResourceCollection.Add(relation, resource);
            return this;
        }

        public IHalResourceBuilder IncludeEmbeddedWithMultipleResources(HalRelation relation, IEnumerable<HalEmbeddedResource> resources)
        {
            if (relation == null)
            {
                throw new ArgumentNullException("relation");
            }
            if (resources == null)
            {
                throw new ArgumentNullException("resources");
            }
            _embeddedResourceCollection.Add(relation, resources);
            return this;
        }

        public HalResource Build()
        {
            HalResource result;
            if (_embeddedResourceCollection.Count > 0)
            {
                result = new HalResource(_resource, _linkCollection, _embeddedResourceCollection);
            }
            else
            {
                result = new HalResource(_resource, _linkCollection);
            }
            return result;
        }
    }
}
