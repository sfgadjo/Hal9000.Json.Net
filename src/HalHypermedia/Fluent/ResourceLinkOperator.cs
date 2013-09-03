using System;

namespace Hal9000.Json.Net.Fluent {
    public class ResourceLinkOperator : IResourceLinkOperator {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly HalRelation _embeddedRelation;
        private readonly HalRelation _linkRelation;
        private readonly bool _predicate;

        internal ResourceLinkOperator ( FluentHalDocumentBuilder builder,
                                        IHalEmbeddedResourceBuilder embeddedResourceBuilder, HalRelation embeddedRelation,
                                        HalRelation linkRelation, bool predicate )
            : this( builder, embeddedResourceBuilder, embeddedRelation, predicate ) {

            if ( linkRelation == null ) {
                throw new ArgumentNullException( "linkRelation" );
            }

            _linkRelation = linkRelation;
        }

        internal ResourceLinkOperator ( FluentHalDocumentBuilder builder,
                                        IHalEmbeddedResourceBuilder embeddedResourceBuilder, HalRelation embeddedRelation,
                                        bool predicate ) {
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

        public IResourceLinkChoice Having {
            get {
                return new ResourceLinkChoice( _builder, _embeddedResourceBuilder, _embeddedRelation, _linkRelation, _predicate );
            }
        }

        public IResourceHaving When ( Func<bool> predicate ) {
            if ( predicate == null ) {
                throw new ArgumentNullException( "predicate" );
            }
            bool predicateResult = predicate();
            return new ResourceLinkOperator( _builder, _embeddedResourceBuilder, _embeddedRelation, _linkRelation,
                                             predicateResult );
        }
    }
}