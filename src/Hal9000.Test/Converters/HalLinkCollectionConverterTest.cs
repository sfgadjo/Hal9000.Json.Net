using System;
using System.IO;
using System.Text;
using Hal9000.Json.Net.Converters;
using Hal9000.Json.Net.Impl;
using NUnit.Framework;
using Newtonsoft.Json;
using Moq;

namespace Hal9000.Json.Net.Test.Converters {

    [TestFixture]
    public class HalLinkCollectionConverterTest {

        [TestCase]
        public void WritingJsonWhenTargetIsNotHalLinkCollectionThrowsException() {

            var fakeWriter = new Mock<JsonWriter>();
            var fakeSerializer = new Mock<JsonSerializer>();
            var converter = new HalLinkCollectionConverter();
            Assert.Throws<InvalidOperationException>(() => converter.WriteJson(fakeWriter.Object, new object(), fakeSerializer.Object));
        }

        [TestCase]
        public void CanWriteJsonForASingleItemCollection() {

            var converter = new HalLinkCollectionConverter();

            const string expectedRelationValue = "my relation";
            var expectedRelation = new HalRelation(expectedRelationValue);
            const string expectedHref = "http://hal.hal";
            var expectedLink = new HalLink(expectedHref);
            var collection = new HalLinkCollection {{expectedRelation, expectedLink}};

            var serializer = new JsonSerializer();
            var builder = new StringBuilder();
            using (var textWriter = new StringWriter(builder))
            using (var writer = new JsonTextWriter(textWriter)) {
                converter.WriteJson(writer, collection, serializer);
            }

            var actual = builder.ToString();
            string expectedOutput =
                String.Format(
                    "{{\"{0}\":{{\"Href\":\"{1}\",\"Title\":null,\"Profile\":null,\"Hreflang\":null,\"Templated\":null}}}}",
                    expectedRelationValue, expectedHref);
            Assert.That(actual, Is.EqualTo(expectedOutput));
        }

        [TestCase]
        public void CanWriteJsonForMultipleItemCollection() {
            var converter = new HalLinkCollectionConverter();

            const string expectedRelationValue = "my relation";
                        const string expectedHref = "http://hal.hal";

            const string expectedRelationValue2 = "my relation 2";
            const string expectedHref2 = "http://hal.hal2";

            var collection = new HalLinkCollection { { new HalRelation( expectedRelationValue ), new HalLink( expectedHref ) }, 
            { new HalRelation(expectedRelationValue2), new HalLink(expectedHref2) } };

            var serializer = new JsonSerializer();
            var builder = new StringBuilder();
            using ( var textWriter = new StringWriter( builder ) )
            using ( var writer = new JsonTextWriter( textWriter ) ) {
                converter.WriteJson( writer, collection, serializer );
            }

            var actual = builder.ToString();

            string expectedOutput =
                String.Format(
                    "{{\"{0}\":{{\"Href\":\"{1}\",\"Title\":null,\"Profile\":null,\"Hreflang\":null,\"Templated\":null}},\"{2}\":{{\"Href\":\"{3}\",\"Title\":null,\"Profile\":null,\"Hreflang\":null,\"Templated\":null}}}}",
                    expectedRelationValue, expectedHref, expectedRelationValue2, expectedHref2);

            Assert.That( actual, Is.EqualTo( expectedOutput ) );
        }
    }
}
