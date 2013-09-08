/*
The MIT License (MIT)

Copyright (c) 2013 Trevel Beshore

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */

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