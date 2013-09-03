using System;

namespace Hal9000.Json.Net.Fluent {
    public class EmbeddedResourceChoice : IEmbeddedResourceChoice {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _embeddedRelation;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly bool _predicate;

        internal EmbeddedResourceChoice ( FluentHalDocumentBuilder builder, HalRelation embeddedRelation,
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

        public IResourceLinkOperator WithSelfRelation () {
            return new ResourceLinkOperator( _builder, _embeddedResourceBuilder, _embeddedRelation,
                                             HalRelation.CreateSelfRelation(), _predicate );
        }

        public IResourceLinkOperator WithLinkRelation ( string relationValue ) {
            return new ResourceLinkOperator( _builder, _embeddedResourceBuilder, _embeddedRelation,
                                             new HalRelation( relationValue ), _predicate );
        }

        public IResourceLinkRelationOperator Resource ( IHalResource resource ) {
            if ( resource == null ) {
                throw new ArgumentNullException( "resource" );
            }
            //add the embedded resource we have been building, then create a new resource builder
            //to start again under the same relation.
            if ( _predicate ) {
                _builder.addEmbeddedResource( _embeddedRelation, _embeddedResourceBuilder );
            }

            var embeddedResourceBuilder = new HalEmbeddedResourceBuilder( resource );
            return new ResourceLinkRelationOperator( _builder, embeddedResourceBuilder, _embeddedRelation, _predicate );
        }
    }
}