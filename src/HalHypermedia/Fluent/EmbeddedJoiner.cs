using System;

namespace Hal9000.Json.Net.Fluent {
    public class EmbeddedJoiner : IEmbeddedJoiner {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly HalRelation _embeddedRelation;
        private readonly bool _predicate;

        internal EmbeddedJoiner ( FluentHalDocumentBuilder builder, IHalEmbeddedResourceBuilder embeddedResourceBuilder,
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

        public HalDocument BuildDocument () {
            addEmbeddedResourceToDocument();
            return _builder.BuildDocument();
        }

        public IEmbeddedResourceChoice Also {
            get {
                return new EmbeddedResourceChoice( _builder, _embeddedRelation, _embeddedResourceBuilder, _predicate );
            }
        }

        public IRelationBuilder And {

            get {
                addEmbeddedResourceToDocument();
                return _builder;
            }
        }

        private void addEmbeddedResourceToDocument () {
            if ( _predicate ) {
                _builder.addEmbeddedResource( _embeddedRelation, _embeddedResourceBuilder );
            }
        }
    }
}