using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HalHypermedia.Extensions;
using Newtonsoft.Json;

namespace HalHypermedia.Converters {
    internal sealed class HalLinkCollectionConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

            var linkCollection = value as HalLinkCollection;
            if (linkCollection == null) {
                const string format = "The target value is not of the expected type. Expected type: {0}";
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, format,
                                                                  typeof (HalLinkCollection).Name));
            }

            writer.WriteStartObject();
            foreach (var linkPair in linkCollection) {

                writer.WritePropertyName(linkPair.Key.Value);
                serializer.Serialize(writer, linkPair.Value);
                //bool isEnumerable = linkPair.Value is IEnumerable<HalLink>;
                //if (isEnumerable) {
                //    //writer.WriteStartArray();
                //    var links = (IEnumerable<HalLink>) linkPair.Value;
                //    serializer.Serialize(writer, links);
                //    //writer.WriteEndArray();
                //} else {
                //    serializer.Serialize( writer, linkPair.Value );
                //    //Type type = typeof (HalLink);
                //    //type.GetProperties().ToList().ForEach(propInfo => {
                           
                //    //        //var propertyValue = propInfo.GetValue(linkPair.Value, null);
                //    //        //if (propertyValue != null) {
                //    //        //    string propertyName = propInfo.GetJsonPropertyName();
                //    //        //    writer.WritePropertyName(propertyName);
                                
                //    //        //}
                //    //    });
                //}
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer) {
            return reader.Value;
        }

        public override bool CanConvert(Type objectType) {
            return (typeof (HalLinkCollection)).IsAssignableFrom(objectType);
        }
    }
}