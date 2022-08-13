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

namespace Hal9000.Json.Net.Fluent {

    /// <summary>
    /// Defines the behavior for composition of links on a resource.
    /// </summary>
    public interface IResourceLinkRelationOperator {

        /// <summary>
        /// Compose a 'self' link on the resource.
        /// </summary>
        /// <returns>Additional composition options.</returns>
        IResourceLinkOperator WithSelfRelation();

        /// <summary>
        /// Compose a link on the resource.
        /// </summary>
        /// <param name="relationValue">The relation of the link to the resource.</param>
        /// <returns>Additional composition options.</returns>
        IResourceLinkOperator WithLinkRelation ( string relationValue );
    }
}