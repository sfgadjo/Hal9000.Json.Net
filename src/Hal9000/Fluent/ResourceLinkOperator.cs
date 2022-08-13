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
    public class ResourceLinkOperator : IResourceLinkOperator {
        private readonly FluentHalDocumentBuilder _builder;
        private readonly IHalEmbeddedResourceBuilder _embeddedResourceBuilder;
        private readonly HalRelation _embeddedRelation;
        private readonly HalRelation _linkRelation;
        private readonly bool _predicate;

        internal ResourceLinkOperator ( FluentHalDocumentBuilder builder,
                                        IHalEmbeddedResourceBuilder embeddedResourceBuilder, HalRelation embeddedRelation,
                                        HalRelation linkRelation, bool predicate )
            : this( builder, embeddedResourceBuilder, embeddedRelation, predicate ) {

            if ( linkRelation == null ) {
                throw new ArgumentNullException( "linkRelation" );
            }

            _linkRelation = linkRelation;
        }

        internal ResourceLinkOperator ( FluentHalDocumentBuilder builder,
                                        IHalEmbeddedResourceBuilder embeddedResourceBuilder, HalRelation embeddedRelation,
                                        bool predicate ) {
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

        public IResourceLinkChoice Having {
            get {
                return new ResourceLinkChoice( _builder, _embeddedResourceBuilder, _embeddedRelation, _linkRelation, _predicate );
            }
        }

        public IResourceHaving When ( Func<bool> predicate ) {
            if ( predicate == null ) {
                throw new ArgumentNullException( "predicate" );
            }
            bool predicateResult = predicate();
            return new ResourceLinkOperator( _builder, _embeddedResourceBuilder, _embeddedRelation, _linkRelation,
                                             predicateResult );
        }
    }
}