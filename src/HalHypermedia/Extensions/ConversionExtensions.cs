using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Hal9000.Json.Net.Extensions {
    internal static class ConversionExtensions {
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
