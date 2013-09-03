using System;
using System.Collections.Generic;

namespace Hal9000.Json.Net.Fluent {
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

        public ILinkJoiner Link ( HalLink link ) {
            if ( link == null ) {
                throw new ArgumentNullException( "link" );
            }
            if ( _predicate ) {
                _builder.includeRelationWithSingleLink( _relation, link );
            }
            return new LinkJoiner( _builder );
        }

        public ILinkJoiner Links ( IEnumerable<HalLink> links ) {
            if ( _predicate ) {
                _builder.includeRelationWithMultipleLinks( _relation, links );
            }
            return new LinkJoiner( _builder );
        }
    }
}