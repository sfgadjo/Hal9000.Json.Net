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
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hal9000.Json.Net.Extensions {

    /// <summary>
    /// Extension methods that assist in the conversion of objects to JSON.
    /// </summary>
    internal static class ConversionExtensions {

        /// <summary>
        /// Converts the target to camel-case.
        /// </summary>
        /// <param name="target">A string to be converted.</param>
        /// <returns>The target string camel-casing.</returns>
        public static string ConvertToCamelCase(this string target) {

            if (String.IsNullOrEmpty(target)) {
                return target;
            }

            if (String.IsNullOrWhiteSpace(target)) {
                return String.Empty;
            }

            char[] chars = target.ToCharArray();
            chars[0] = Char.ToLower(chars[0]);
            return new string(chars);
        }

        /// <summary>
        /// Gets the property name to use in a JSON representation from the given <see cref="MemberInfo"/>.
        /// Note: Makes use of <see cref="JsonPropertyAttribute"/> if the property has been decorated with that attribute.
        /// </summary>
        /// <param name="target">The <see cref="MemberInfo"/> from which to get the property name.</param>
        /// <param name="contractResolver">An object that resolves JSON contracts for a given type.</param>
        /// <returns>A property name.</returns>
        public static string GetJsonPropertyName(this MemberInfo target, IContractResolver contractResolver) {
            if (target == null) {
                throw new ArgumentNullException("target");
            }

            string propertyName;
            if (contractResolver is CamelCasePropertyNamesContractResolver) {
                propertyName = target.Name.ConvertToCamelCase();
            } else {
                propertyName = target.Name;
            }

            var jsonPropertyAttribute =
                (JsonPropertyAttribute)
                target.GetCustomAttributes(typeof (JsonPropertyAttribute), true).FirstOrDefault();
            if (jsonPropertyAttribute != null) {
                propertyName = jsonPropertyAttribute.PropertyName;
            }
            return propertyName;
        }
    }
}
