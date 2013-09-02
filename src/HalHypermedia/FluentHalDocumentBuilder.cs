using System;
using System.Collections.Generic;
using System.Linq;
using Hal9000.Json.Net.Fluent;

namespace Hal9000.Json.Net {
    public class FluentHalDocumentBuilder : IFluentHalDocumentBuilder {
        private readonly IHalDocumentBuilder _documentBuilder;

        private readonly IDictionary<HalRelation, IList<HalEmbeddedResource>> _embeddedResources =
            new Dictionary<HalRelation, IList<HalEmbeddedResource>>();

        public FluentHalDocumentBuilder(IHalResource resource) {
            if (resource == null) {
                throw new ArgumentNullException("resource");
            }
            _documentBuilder = new HalDocumentBuilder(resource);
        }

        internal FluentHalDocumentBuilder(IHalDocumentBuilder documentBuilder) {
            if (documentBuilder == null) {
                throw new ArgumentNullException("documentBuilder");
            }
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

    public class EmbeddedOperator : IEmbeddedOperator {
        private readonly bool _predicate;
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _relation;

        internal EmbeddedOperator(FluentHalDocumentBuilder builder, HalRelation relation)
            : this(builder, relation, true) {

        }

        private EmbeddedOperator ( FluentHalDocumentBuilder builder, HalRelation relation, bool predicate ) {
            if ( builder == null ) {
                throw new ArgumentNullException( "builder" );
            }
            if ( relation == null ) {
                throw new ArgumentNullException( "relation" );
            }
            _builder = builder;
            _relation = relation;
            _predicate = predicate;
        }

        public IEmbeddedRelationChoice Having {
            get { return new EmbeddedRelationChoice(_builder, _relation, _predicate); }
        }

        public IEmbeddedHaving When(Func<bool> predicate) {
            if (predicate == null) {
                throw new ArgumentNullException("predicate");
            }

            bool predicateResult = predicate();
            return new EmbeddedOperator(_builder, _relation, predicateResult);
        }
    }

    public class EmbeddedRelationChoice : IEmbeddedRelationChoice {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _embeddedRelation;
        private readonly bool _predicate;
        
        internal EmbeddedRelationChoice(FluentHalDocumentBuilder builder, HalRelation embeddedRelation, bool predicate) {
            if ( builder == null ) {
                throw new ArgumentNullException( "builder" );
            }
            if ( embeddedRelation == null ) {
                throw new ArgumentNullException( "embeddedRelation" );
            }
            _builder = builder;
            _embeddedRelation = embeddedRelation;
            _predicate = predicate;
        }

        public IResourceLinkRelationOperator Resource(IHalResource resource) {
            if (resource == null) {
                throw new ArgumentNullException("resource");
            }
            var embeddedResourceBuilder = new HalEmbeddedResourceBuilder(resource);
            return new ResourceLinkRelationOperator(_builder, embeddedResourceBuilder, _embeddedRelation, _predicate);
        }

        public IResourceOperator MultipleResources {
            get { return new ResourceOperator(_builder, _embeddedRelation, _predicate); }
        }
    }

    public class ResourceLinkRelationOperator : IResourceLinkRelationOperator {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly HalRelation _embeddedRelation;
        private readonly bool _predicate;

        internal ResourceLinkRelationOperator(FluentHalDocumentBuilder builder,
                                              IHalEmbeddedResourceBuilder embeddedResourceBuilder,
                                              HalRelation embeddedRelation, bool predicate) {
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
            _predicate = predicate;
        }

        public IResourceLinkOperator WithSelfRelation() {
            return new ResourceLinkOperator(_builder, _embeddedResourceBuilder, _embeddedRelation,
                                            HalRelation.CreateSelfRelation(), _predicate);
        }

        public IResourceLinkOperator WithLinkRelation(string relationValue) {
            return new ResourceLinkOperator(_builder, _embeddedResourceBuilder, _embeddedRelation,
                                            new HalRelation(relationValue), _predicate);
        }
    }

    public class ResourceLinkOperator : IResourceLinkOperator {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly HalRelation _embeddedRelation;
        private readonly HalRelation _linkRelation;
        private readonly bool _predicate;

        internal ResourceLinkOperator(FluentHalDocumentBuilder builder,
                                      IHalEmbeddedResourceBuilder embeddedResourceBuilder, HalRelation embeddedRelation,
                                      HalRelation linkRelation, bool predicate)
            : this(builder, embeddedResourceBuilder, embeddedRelation, predicate) {

            if (linkRelation == null) {
                throw new ArgumentNullException("linkRelation");
            }

            _linkRelation = linkRelation;
        }

        internal ResourceLinkOperator(FluentHalDocumentBuilder builder,
                                      IHalEmbeddedResourceBuilder embeddedResourceBuilder, HalRelation embeddedRelation,
                                      bool predicate) {
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
            _predicate = predicate;
        }

        public IResourceLinkChoice Having {
            get { return new ResourceLinkChoice(_builder, _embeddedResourceBuilder, _embeddedRelation, _linkRelation, _predicate); }
        }

        public IResourceHaving When(Func<bool> predicate) {
            if (predicate == null) {
                throw new ArgumentNullException("predicate");
            }
            bool predicateResult = predicate();
            return new ResourceLinkOperator(_builder, _embeddedResourceBuilder, _embeddedRelation, _linkRelation,
                                            predicateResult );
        }
    }

    public class ResourceLinkChoice : IResourceLinkChoice {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly HalRelation _embeddedRelation;
        private readonly HalRelation _linkRelation;
        private readonly bool _predicate;

        internal ResourceLinkChoice ( FluentHalDocumentBuilder builder, IHalEmbeddedResourceBuilder embeddedResourceBuilder,
                                  HalRelation embeddedRelation, HalRelation linkRelation, bool predicate) {
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
            _predicate = predicate;
        }

        public IEmbeddedJoiner Link(HalLink link) {
            if (link == null) {
                throw new ArgumentNullException("link");
            }

            if (_predicate) {
                _embeddedResourceBuilder.IncludeRelationWithSingleLink(_linkRelation, link);
            }

            return new EmbeddedJoiner(_builder, _embeddedResourceBuilder, _embeddedRelation, _predicate);
        }

        public IEmbeddedJoiner Links(IEnumerable<HalLink> links) {
            if (links == null) {
                throw new ArgumentNullException("links");
            }

            if (_predicate){
                _embeddedResourceBuilder.IncludeRelationWithMultipleLinks(_linkRelation, links);
            }
            return new EmbeddedJoiner(_builder, _embeddedResourceBuilder, _embeddedRelation, _predicate);
        }
    }

    public class EmbeddedJoiner : IEmbeddedJoiner {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly HalRelation _embeddedRelation;
        private readonly bool _predicate;

        internal EmbeddedJoiner ( FluentHalDocumentBuilder builder, IHalEmbeddedResourceBuilder embeddedResourceBuilder,
                              HalRelation embeddedRelation, bool predicate) {
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
            _predicate = predicate;
        }

        public HalDocument BuildDocument() {
            addEmbeddedResourceToDocument();
            return _builder.BuildDocument();
        }

        public IEmbeddedResourceChoice Also {
            get { return new EmbeddedResourceChoice(_builder, _embeddedRelation, _embeddedResourceBuilder, _predicate); }
        }

        public IRelationBuilder And {

            get {
                addEmbeddedResourceToDocument();
                return _builder;
            }
        }

        private void addEmbeddedResourceToDocument() {
            if (_predicate) {
                _builder.addEmbeddedResource(_embeddedRelation, _embeddedResourceBuilder);
            }
        }
    }

    public class EmbeddedResourceChoice : IEmbeddedResourceChoice {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _embeddedRelation;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly bool _predicate;

        internal EmbeddedResourceChoice(FluentHalDocumentBuilder builder, HalRelation embeddedRelation,
                                      IHalEmbeddedResourceBuilder embeddedResourceBuilder, bool predicate) {
            if ( builder == null ) {
                throw new ArgumentNullException( "builder" );
            }
            if ( embeddedRelation == null ) {
                throw new ArgumentNullException( "embeddedRelation" );
            }
            if ( embeddedResourceBuilder == null ) {
                throw new ArgumentNullException( "embeddedResourceBuilder" );
            }
            _builder = builder;
            _embeddedRelation = embeddedRelation;
            _embeddedResourceBuilder = embeddedResourceBuilder;
            _predicate = predicate;
        }

        public IResourceLinkOperator WithSelfRelation() {
            return new ResourceLinkOperator(_builder, _embeddedResourceBuilder, _embeddedRelation,
                                            HalRelation.CreateSelfRelation(), _predicate);
        }

        public IResourceLinkOperator WithLinkRelation(string relationValue) {
            return new ResourceLinkOperator(_builder, _embeddedResourceBuilder, _embeddedRelation,
                                            new HalRelation(relationValue), _predicate);
        }

        public IResourceLinkRelationOperator Resource(IHalResource resource) {
            if (resource == null) {
                throw new ArgumentNullException("resource");
            }
            //add the embedded resource we have been building, then create a new resource builder
            //to start again under the same relation.
            if (_predicate) {
                _builder.addEmbeddedResource(_embeddedRelation, _embeddedResourceBuilder);
            }

            var embeddedResourceBuilder = new HalEmbeddedResourceBuilder(resource);
            return new ResourceLinkRelationOperator(_builder, embeddedResourceBuilder, _embeddedRelation, _predicate);
        }
    }

    public class ResourceOperator : IResourceOperator {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _embeddedRelation;
        private readonly bool _predicate;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;

        internal ResourceOperator ( FluentHalDocumentBuilder builder, HalRelation embeddedRelation,
                                 IHalEmbeddedResourceBuilder embeddedResourceBuilder, bool predicate ) {
            if ( builder == null ) {
                throw new ArgumentNullException( "builder" );
            }

            if ( embeddedRelation == null ) {
                throw new ArgumentNullException( "embeddedRelation" );
            }
            if ( embeddedResourceBuilder == null ) {
                throw new ArgumentNullException( "embeddedResourceBuilder" );
            }
            _builder = builder;
            _embeddedRelation = embeddedRelation;
            _embeddedResourceBuilder = embeddedResourceBuilder;
            _predicate = predicate;
        }

        internal ResourceOperator(FluentHalDocumentBuilder builder, HalRelation embeddedRelation, bool predicate) {
            if ( builder == null ) {
                throw new ArgumentNullException( "builder" );
            }

            if ( embeddedRelation == null ) {
                throw new ArgumentNullException( "embeddedRelation" );
            }
            _builder = builder;
            _embeddedRelation = embeddedRelation;
            _predicate = predicate;
        }

        public IResourceLinkRelationOperator Resource(IHalResource resource) {
            if (resource == null) {
                throw new ArgumentNullException("resource");
            }
            var embeddedResourceBuilder = new HalEmbeddedResourceBuilder(resource);

            return new ResourceLinkRelationOperator( _builder, embeddedResourceBuilder, _embeddedRelation, _predicate );
        }

        public IResourceLinkOperator WithSelfRelation() {
            return new ResourceLinkOperator(_builder, _embeddedResourceBuilder, HalRelation.CreateSelfRelation(), _predicate);
        }

        public IResourceLinkOperator WithLinkRelation(string relationValue) {
            return new ResourceLinkOperator( _builder, _embeddedResourceBuilder, new HalRelation( relationValue ), _predicate);
        }
    }

    public class LinkOperator : ILinkOperator {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _relation;
        private readonly bool _predicate;

        internal LinkOperator(FluentHalDocumentBuilder builder, HalRelation relation) : this(builder, relation, true) {
           
        }

        private LinkOperator(FluentHalDocumentBuilder builder, HalRelation relation, bool predicate) {
            if ( builder == null ) {
                throw new ArgumentNullException( "builder" );
            }
            if ( relation == null ) {
                throw new ArgumentNullException( "relation" );
            }

            _builder = builder;
            _relation = relation;
            _predicate = predicate;
        }

        public ILinkChoice Having {
            get { return new LinkChoice(_builder, _relation, _predicate); }
        }

        public ILinkHaving When(Func<bool> predicate) {
            if (predicate == null) {
                throw new ArgumentNullException("predicate");
            }

            bool predicateResult = predicate();
            return new LinkOperator( _builder, _relation, predicateResult );
        }
    }

    public class LinkChoice : ILinkChoice {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _relation;
        private readonly bool _predicate;

        internal LinkChoice ( FluentHalDocumentBuilder builder, HalRelation relation, bool predicate ) {
            if ( builder == null ) {
                throw new ArgumentNullException( "builder" );
            }
            if ( relation == null ) {
                throw new ArgumentNullException( "relation" );
            }

            _builder = builder;
            _relation = relation;
            _predicate = predicate;
        }

        public ILinkJoiner Link(HalLink link) {
            if (link == null) {
                throw new ArgumentNullException("link");
            }
            if (_predicate) {
                _builder.includeRelationWithSingleLink(_relation, link);
            }
            return new LinkJoiner(_builder);
        }

        public ILinkJoiner Links(IEnumerable<HalLink> links) {
            if (_predicate) {
                _builder.includeRelationWithMultipleLinks(_relation, links);
            }
            return new LinkJoiner(_builder);
        }
    }

    public class LinkJoiner : ILinkJoiner {
        private readonly FluentHalDocumentBuilder _builder;

        internal LinkJoiner(FluentHalDocumentBuilder builder) {
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