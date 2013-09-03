using System;

namespace Hal9000.Json.Net.Fluent {
    public class EmbeddedOperator : IEmbeddedOperator {
        private readonly bool _predicate;
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _relation;

        internal EmbeddedOperator ( FluentHalDocumentBuilder builder, HalRelation relation )
            : this( builder, relation, true ) {

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
            get {
                return new EmbeddedRelationChoice( _builder, _relation, _predicate );
            }
        }

        public IEmbeddedHaving When ( Func<bool> predicate ) {
            if ( predicate == null ) {
                throw new ArgumentNullException( "predicate" );
            }

            bool predicateResult = predicate();
            return new EmbeddedOperator( _builder, _relation, predicateResult );
        }
    }
}