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