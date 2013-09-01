using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

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
        /// <returns>A property name.</returns>
        public static string GetJsonPropertyName(this MemberInfo target) {
            if (target == null) {
                throw new ArgumentNullException("target");
            }

            string propertyName = target.Name.ConvertToCamelCase();
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
