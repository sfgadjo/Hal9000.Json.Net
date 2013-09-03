using System;
using System.Collections.Generic;
using System.Linq;
using Hal9000.Json.Net.Fluent;

namespace Hal9000.Json.Net {

    /// <summary>
    /// Builds a <see cref="HalDocument"/> using a fluent inteface.
    /// </summary>
    public class FluentHalDocumentBuilder : IFluentHalDocumentBuilder {
        private readonly IHalDocumentBuilder _documentBuilder;

        private readonly IDictionary<HalRelation, IList<HalEmbeddedResource>> _embeddedResources =
            new Dictionary<HalRelation, IList<HalEmbeddedResource>>();

        /// <summary>
        /// Creates an instance of <see cref="FluentHalDocumentBuilder"/>.
        /// </summary>
        /// <param name="resource">The root resource of the document that will be built.</param>
        public FluentHalDocumentBuilder(IHalResource resource) {
            if (resource == null) {
                throw new ArgumentNullException("resource");
            }
            _documentBuilder = new HalDocumentBuilder(resource);
        }


        /// <summary>
        /// Creates an instance of <see cref="FluentHalDocumentBuilder"/>.
        /// </summary>
        /// <param name="documentBuilder">An object that can build a <see cref="HalDocument"/>.</param>
        internal FluentHalDocumentBuilder(IHalDocumentBuilder documentBuilder) {
            if (documentBuilder == null) {
                throw new ArgumentNullException("documentBuilder");
            }
            _documentBuilder = documentBuilder;
        }

        /// <summary>
        /// Begins the composition of a 'self' link for the resource.
        /// </summary>
        /// <returns>Further options for composing the link.</returns>
        public ILinkOperator WithSelfRelation() {
            return new LinkOperator(this, HalRelation.CreateSelfRelation());
        }

        /// <summary>
        /// Begins the composition of a link for the resource.
        /// </summary>
        /// <param name="relationValue">The value of the relation, or how the link relates to the resource.</param>
        /// <returns></returns>
        public ILinkOperator WithLinkRelation(string relationValue) {
            return new LinkOperator(this, new HalRelation(relationValue));
        }

        public IEmbeddedOperator WithEmbeddedRelation(string relationValue) {
            return new EmbeddedOperator(this, new HalRelation(relationValue));
        }

        public HalDocument BuildDocument() {

            foreach (var pair in _embeddedResources) {
                int count = pair.Value.Count;
                if (count > 1) {
                    _documentBuilder.IncludeEmbeddedWithMultipleResources(pair.Key, pair.Value);
                } else if (count == 1) {
                    _documentBuilder.IncludeEmbeddedWithSingleResource(pair.Key, pair.Value.First());
                }
            }
            return _documentBuilder.Build();
        }

        internal void addEmbeddedResource(HalRelation embeddedRelation,
                                          IHalEmbeddedResourceBuilder embeddedResourceBuilder) {
            if (embeddedRelation == null) {
                throw new ArgumentNullException("embeddedRelation");
            }
            if (embeddedResourceBuilder == null) {
                throw new ArgumentNullException("embeddedResourceBuilder");
            }
            IList<HalEmbeddedResource> resources;
            var embeddedResource = new HalEmbeddedResource(embeddedResourceBuilder);

            bool hasRelationAlready = _embeddedResources.TryGetValue(embeddedRelation, out resources);
            if (hasRelationAlready) {
                resources.Add(embeddedResource);
            } else {
                _embeddedResources.Add(embeddedRelation, new List<HalEmbeddedResource> {embeddedResource});
            }
        }

        internal void includeRelationWithSingleLink(HalRelation relation, HalLink link) {
            if (relation == null) {
                throw new ArgumentNullException("relation");
            }
            if (link == null) {
                throw new ArgumentNullException("link");
            }

            _documentBuilder.IncludeRelationWithSingleLink(relation, link);
        }

        internal void includeRelationWithMultipleLinks(HalRelation relation, IEnumerable<HalLink> links) {
            if (relation == null) {
                throw new ArgumentNullException("relation");
            }
            if (links == null) {
                throw new ArgumentNullException("links");
            }
            _documentBuilder.IncludeRelationWithMultipleLinks(relation, links);
        }
    }
}