using System;

namespace Hal9000.Json.Net.Fluent {
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

        internal ResourceOperator ( FluentHalDocumentBuilder builder, HalRelation embeddedRelation, bool predicate ) {
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

        public IResourceLinkRelationOperator Resource ( IHalResource resource ) {
            if ( resource == null ) {
                throw new ArgumentNullException( "resource" );
            }
            var embeddedResourceBuilder = new HalEmbeddedResourceBuilder( resource );

            return new ResourceLinkRelationOperator( _builder, embeddedResourceBuilder, _embeddedRelation, _predicate );
        }

        public IResourceLinkOperator WithSelfRelation () {
            return new ResourceLinkOperator( _builder, _embeddedResourceBuilder, HalRelation.CreateSelfRelation(), _predicate );
        }

        public IResourceLinkOperator WithLinkRelation ( string relationValue ) {
            return new ResourceLinkOperator( _builder, _embeddedResourceBuilder, new HalRelation( relationValue ), _predicate );
        }
    }
}