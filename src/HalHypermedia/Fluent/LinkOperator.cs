using System;

namespace Hal9000.Json.Net.Fluent {
    public class LinkOperator : ILinkOperator {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly HalRelation _relation;
        private readonly bool _predicate;

        internal LinkOperator ( FluentHalDocumentBuilder builder, HalRelation relation )
            : this( builder, relation, true ) {

        }

        private LinkOperator ( FluentHalDocumentBuilder builder, HalRelation relation, bool predicate ) {
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
            get {
                return new LinkChoice( _builder, _relation, _predicate );
            }
        }

        public ILinkHaving When ( Func<bool> predicate ) {
            if ( predicate == null ) {
                throw new ArgumentNullException( "predicate" );
            }

            bool predicateResult = predicate();
            return new LinkOperator( _builder, _relation, predicateResult );
        }
    }
}