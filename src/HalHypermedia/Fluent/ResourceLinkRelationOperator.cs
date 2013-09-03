using System;

namespace Hal9000.Json.Net.Fluent {
    public class ResourceLinkRelationOperator : IResourceLinkRelationOperator {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly HalRelation _embeddedRelation;
        private readonly bool _predicate;

        internal ResourceLinkRelationOperator ( FluentHalDocumentBuilder builder,
                                                IHalEmbeddedResourceBuilder embeddedResourceBuilder,
                                                HalRelation embeddedRelation, bool predicate ) {
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

        public IResourceLinkOperator WithSelfRelation () {
            return new ResourceLinkOperator( _builder, _embeddedResourceBuilder, _embeddedRelation,
                                             HalRelation.CreateSelfRelation(), _predicate );
        }

        public IResourceLinkOperator WithLinkRelation ( string relationValue ) {
            return new ResourceLinkOperator( _builder, _embeddedResourceBuilder, _embeddedRelation,
                                             new HalRelation( relationValue ), _predicate );
        }
    }
}