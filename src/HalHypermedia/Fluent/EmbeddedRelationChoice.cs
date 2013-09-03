using System;

namespace Hal9000.Json.Net.Fluent {
    public class EmbeddedRelationChoice : IEmbeddedRelationChoice {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _embeddedRelation;
        private readonly bool _predicate;

        internal EmbeddedRelationChoice ( FluentHalDocumentBuilder builder, HalRelation embeddedRelation, bool predicate ) {
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

        public IResourceOperator MultipleResources {
            get {
                return new ResourceOperator( _builder, _embeddedRelation, _predicate );
            }
        }
    }
}