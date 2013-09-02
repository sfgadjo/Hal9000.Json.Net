using System;
using System.Collections.Generic;
using System.Linq;
using Hal9000.Json.Net.Fluent;

namespace Hal9000.Json.Net {
    public class FluentHalDocumentBuilder : IFluentHalDocumentBuilder {
        private readonly IHalDocumentBuilder _documentBuilder;
        private readonly IDictionary<HalRelation, IList<HalEmbeddedResource>> _embeddedResources = new Dictionary<HalRelation, IList<HalEmbeddedResource>>(); 

        public FluentHalDocumentBuilder(IHalResource resource) {
            if (resource == null) {
                throw new ArgumentNullException("resource");
            }
            _documentBuilder = new HalDocumentBuilder(resource);
        }

        internal FluentHalDocumentBuilder ( IHalDocumentBuilder documentBuilder) {
            _documentBuilder = documentBuilder;
        }

        public ILinkOperator WithSelfRelation() {
            return new LinkOperator(this, HalRelation.CreateSelfRelation());
        }

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
                }else if (count == 1) {
                    _documentBuilder.IncludeEmbeddedWithSingleResource(pair.Key, pair.Value.First());
                }
            }
            return _documentBuilder.Build();
        }

        public IRelationBuilder When(Func<bool> predicate) {
            //TODO: this should move to end of flow
            return this;
        }

        //internal IHalDocumentBuilder documentBuilder {
        //    get { return _documentBuilder; }
        //}

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
                resources.Add( embeddedResource );
            } else {
                _embeddedResources.Add( embeddedRelation, new List<HalEmbeddedResource> { embeddedResource} );
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

    public class EmbeddedOperator : IEmbeddedOperator {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _relation;

        internal EmbeddedOperator ( FluentHalDocumentBuilder builder, HalRelation relation ) {
            if (builder == null) {
                throw new ArgumentNullException("builder");
            }
            if (relation == null) {
                throw new ArgumentNullException("relation");
            }
            _builder = builder;
            _relation = relation;
        }

        public IEmbeddedRelationChoice Having {
            get { return new EmbeddedRelationChoice(_builder, _relation); }
        }
    }

    public class EmbeddedRelationChoice : IEmbeddedRelationChoice {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _embeddedRelation;

        internal EmbeddedRelationChoice (FluentHalDocumentBuilder builder, HalRelation embeddedRelation) {
            if (builder == null) {
                throw new ArgumentNullException("builder");
            }
            if ( embeddedRelation == null ) {
                throw new ArgumentNullException( "embeddedRelation" );
            }
            _builder = builder;
            _embeddedRelation = embeddedRelation;
        }

        public IResourceLinkRelationOperator Resource(IHalResource resource) {
            if (resource == null) {
                throw new ArgumentNullException("resource");
            }
            var embeddedResourceBuilder = new HalEmbeddedResourceBuilder( resource );
            return new ResourceLinkRelationOperator( _builder, embeddedResourceBuilder, _embeddedRelation );
        }

        public IResourceOperator MultipleResources {
            get { return new ResourceOperator(_builder, _embeddedRelation); }
        }
    }

    public class ResourceLinkRelationOperator : IResourceLinkRelationOperator {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly HalRelation _embeddedRelation;

        internal ResourceLinkRelationOperator(FluentHalDocumentBuilder builder,
                                              IHalEmbeddedResourceBuilder embeddedResourceBuilder, HalRelation embeddedRelation) {
            if (builder == null) {
                throw new ArgumentNullException("builder");
            }
            if (embeddedResourceBuilder == null) {
                throw new ArgumentNullException("embeddedResourceBuilder");
            }
            if (embeddedRelation == null) {
                throw new ArgumentNullException("embeddedRelation");
            }
            _builder = builder;
            _embeddedResourceBuilder = embeddedResourceBuilder;
            _embeddedRelation = embeddedRelation;
        }

        public IResourceLinkOperator WithSelfRelation() {
            return new ResourceLinkOperator(_builder, _embeddedResourceBuilder, _embeddedRelation, HalRelation.CreateSelfRelation());
        }

        public IResourceLinkOperator WithLinkRelation(string relationValue) {
            return new ResourceLinkOperator( _builder, _embeddedResourceBuilder, _embeddedRelation, new HalRelation(relationValue));
        }
    }

    public class ResourceLinkOperator : IResourceLinkOperator {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly HalRelation _embeddedRelation;
        private readonly HalRelation _linkRelation;

        internal ResourceLinkOperator(FluentHalDocumentBuilder builder,
                                      IHalEmbeddedResourceBuilder embeddedResourceBuilder, HalRelation embeddedRelation,
                                      HalRelation linkRelation)
            : this(builder, embeddedResourceBuilder, embeddedRelation) {

            if (linkRelation == null) {
                throw new ArgumentNullException("linkRelation");
            }

            _linkRelation = linkRelation;
        }

        internal ResourceLinkOperator(FluentHalDocumentBuilder builder,
                                    IHalEmbeddedResourceBuilder embeddedResourceBuilder, HalRelation embeddedRelation ) {
            if ( builder == null ) {
                throw new ArgumentNullException( "builder" );
            }
            if ( embeddedResourceBuilder == null ) {
                throw new ArgumentNullException( "embeddedResourceBuilder" );
            }
            if ( embeddedRelation == null ) {
                throw new ArgumentNullException( "embeddedRelation" );
            }
            _builder = builder;
            _embeddedResourceBuilder = embeddedResourceBuilder;
            _embeddedRelation = embeddedRelation;
        }

        public IResourceLinkChoice Having {
            get { return new ResourceLinkChoice(_builder, _embeddedResourceBuilder, _embeddedRelation, _linkRelation); }
        }
    }

    public class ResourceLinkChoice : IResourceLinkChoice {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly HalRelation _embeddedRelation;
        private readonly HalRelation _linkRelation;

        internal ResourceLinkChoice(FluentHalDocumentBuilder builder, IHalEmbeddedResourceBuilder embeddedResourceBuilder,
                                    HalRelation embeddedRelation, HalRelation linkRelation) {
            if (builder == null) {
                throw new ArgumentNullException("builder");
            }
            if (embeddedResourceBuilder == null) {
                throw new ArgumentNullException("embeddedResourceBuilder");
            }
            if (embeddedRelation == null) {
                throw new ArgumentNullException("embeddedRelation");
            }
            if (linkRelation == null) {
                throw new ArgumentNullException("linkRelation");
            }
            _builder = builder;
            _embeddedResourceBuilder = embeddedResourceBuilder;
            _embeddedRelation = embeddedRelation;
            _linkRelation = linkRelation;
        }

        public IEmbeddedJoiner Link(HalLink link) {
            if (link == null) {
                throw new ArgumentNullException("link");
            }

            _embeddedResourceBuilder.IncludeRelationWithSingleLink(_linkRelation, link);

            return new EmbeddedJoiner(_builder, _embeddedResourceBuilder, _embeddedRelation);
        }

        public IEmbeddedJoiner Links(IEnumerable<HalLink> links) {
            if (links == null) {
                throw new ArgumentNullException("links");
            }

            _embeddedResourceBuilder.IncludeRelationWithMultipleLinks( _linkRelation, links );
            return new EmbeddedJoiner( _builder,  _embeddedResourceBuilder, _embeddedRelation );
        }
    }

    public class EmbeddedJoiner : IEmbeddedJoiner {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly HalRelation _embeddedRelation;

        internal EmbeddedJoiner(FluentHalDocumentBuilder builder, IHalEmbeddedResourceBuilder embeddedResourceBuilder,
                                HalRelation embeddedRelation) {
            if (builder == null) {
                throw new ArgumentNullException("builder");
            }
            if (embeddedResourceBuilder == null) {
                throw new ArgumentNullException("embeddedResourceBuilder");
            }
            if (embeddedRelation == null) {
                throw new ArgumentNullException("embeddedRelation");
            }
            _builder = builder;
            _embeddedResourceBuilder = embeddedResourceBuilder;
            _embeddedRelation = embeddedRelation;
        }

        public HalDocument BuildDocument() {
            addEmbeddedResourceToDocument();
            return _builder.BuildDocument();
        }

        public IEmbeddedResourceChoice Also {
            get {
                return new EmbeddedResourceChoice(_builder, _embeddedRelation, _embeddedResourceBuilder);
            }
        }

        public IRelationBuilder And {

            get {
                addEmbeddedResourceToDocument();
                return _builder;
            }
        }

        private void addEmbeddedResourceToDocument() {
            _builder.addEmbeddedResource( _embeddedRelation, _embeddedResourceBuilder );
        }
    }

    public class EmbeddedResourceChoice : IEmbeddedResourceChoice {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _embeddedRelation;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;

        internal EmbeddedResourceChoice(FluentHalDocumentBuilder builder, HalRelation embeddedRelation,
                                        IHalEmbeddedResourceBuilder embeddedResourceBuilder) {
            if (builder == null) {
                throw new ArgumentNullException("builder");
            }
            if (embeddedRelation == null) {
                throw new ArgumentNullException("embeddedRelation");
            }
            if (embeddedResourceBuilder == null) {
                throw new ArgumentNullException("embeddedResourceBuilder");
            }
            _builder = builder;
            _embeddedRelation = embeddedRelation;
            _embeddedResourceBuilder = embeddedResourceBuilder;
        }

        public IResourceLinkOperator WithSelfRelation() {
            return new ResourceLinkOperator(_builder, _embeddedResourceBuilder, _embeddedRelation, HalRelation.CreateSelfRelation());
        }

        public IResourceLinkOperator WithLinkRelation(string relationValue) {
            return new ResourceLinkOperator( _builder, _embeddedResourceBuilder, _embeddedRelation, new HalRelation(relationValue) );
        }

        public IResourceLinkRelationOperator Resource(IHalResource resource) {
            if (resource == null) {
                throw new ArgumentNullException("resource");
            }
            //add the embedded resource we have been building, then create a new resource builder
            //to start again under the same relation.

            _builder.addEmbeddedResource(_embeddedRelation, _embeddedResourceBuilder);
            var embeddedResourceBuilder = new HalEmbeddedResourceBuilder(resource);
            return new ResourceLinkRelationOperator(_builder, embeddedResourceBuilder, _embeddedRelation);
        }
    }

    public class ResourceOperator : IResourceOperator {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _embeddedRelation;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;

        internal ResourceOperator ( FluentHalDocumentBuilder builder, HalRelation embeddedRelation) {
            if ( builder == null ) {
                throw new ArgumentNullException( "builder" );
            }

            if ( embeddedRelation == null ) {
                throw new ArgumentNullException( "embeddedRelation" );
            }
            _builder = builder;
            _embeddedRelation = embeddedRelation;
        }

        internal ResourceOperator(FluentHalDocumentBuilder builder, HalRelation embeddedRelation,
                                  IHalEmbeddedResourceBuilder embeddedResourceBuilder) {
            if (builder == null) {
                throw new ArgumentNullException("builder");
            }

            if (embeddedRelation == null) {
                throw new ArgumentNullException("embeddedRelation");
            }
            if (embeddedResourceBuilder == null) {
                throw new ArgumentNullException("embeddedResourceBuilder");
            }
            _builder = builder;
            _embeddedRelation = embeddedRelation;
            _embeddedResourceBuilder = embeddedResourceBuilder;
        }

        public IResourceLinkRelationOperator Resource ( IHalResource resource) {
            if ( resource == null ) {
                throw new ArgumentNullException( "resource" );
            }
            var embeddedResourceBuilder = new HalEmbeddedResourceBuilder(resource);

            return new ResourceLinkRelationOperator( _builder, embeddedResourceBuilder, _embeddedRelation );
        }

        public IResourceLinkOperator WithSelfRelation() {
            return new ResourceLinkOperator(_builder, _embeddedResourceBuilder, HalRelation.CreateSelfRelation());
        }

        public IResourceLinkOperator WithLinkRelation(string relationValue) {
            return new ResourceLinkOperator(_builder, _embeddedResourceBuilder, new HalRelation(relationValue));
        }
    }

    public class LinkOperator : ILinkOperator {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _relation;

        internal LinkOperator ( FluentHalDocumentBuilder builder, HalRelation relation ) {
            if (builder == null) {
                throw new ArgumentNullException("builder");
            }
            if (relation == null) {
                throw new ArgumentNullException("relation");
            }
            _builder = builder;
            _relation = relation;
        }

        public ILinkChoice Having {
            get { return new LinkChoice(_builder, _relation); }
        }
    }

    public class LinkChoice : ILinkChoice {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _relation;

        internal LinkChoice ( FluentHalDocumentBuilder builder, HalRelation relation ) {
            if (builder == null) {
                throw new ArgumentNullException("builder");
            }
            if (relation == null) {
                throw new ArgumentNullException("relation");
            }
            _builder = builder;
            _relation = relation;
        }

        public ILinkJoiner Link(HalLink link) {
            if (link == null) {
                throw new ArgumentNullException("link");
            }
            _builder.includeRelationWithSingleLink(_relation, link);
            return new LinkJoiner( _builder );
        }

        public ILinkJoiner Links(IEnumerable<HalLink> links) {
            _builder.includeRelationWithMultipleLinks(_relation, links);
            return new LinkJoiner(_builder);
        }
    }

    public class LinkJoiner : ILinkJoiner {
        private readonly FluentHalDocumentBuilder _builder;

        internal LinkJoiner ( FluentHalDocumentBuilder builder ) {
            if (builder == null) {
                throw new ArgumentNullException("builder");
            }
            _builder = builder;
        }

        public HalDocument BuildDocument() {
            return _builder.BuildDocument();
        }

        public IRelationBuilder And {
            get { return _builder; }
        }
    }
}