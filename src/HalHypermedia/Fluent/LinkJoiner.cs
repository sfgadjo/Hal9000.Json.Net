using System;

namespace Hal9000.Json.Net.Fluent {

    public class LinkJoiner : ILinkJoiner {
        private readonly FluentHalDocumentBuilder _builder;

        internal LinkJoiner ( FluentHalDocumentBuilder builder ) {
            if ( builder == null ) {
                throw new ArgumentNullException( "builder" );
            }
            _builder = builder;
        }

        public HalDocument BuildDocument () {
            return _builder.BuildDocument();
        }

        public IRelationBuilder And {
            get {
                return _builder;
            }
        }
    }
}
