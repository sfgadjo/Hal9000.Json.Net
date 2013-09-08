using System;
using System.IO;
using System.Text;
using Hal9000.Json.Net.Converters;
using Moq;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Hal9000.Json.Net.Test.Converters {

    [TestFixture]
    public class HalEmbeddedResourceConverterTest {
        [TestCase]
        public void WritingJsonWhenTargetIsNotHalEmbeddedResourceThrowsException() {
            var fakeWriter = new Mock<JsonWriter>();
            var fakeSerializer = new Mock<JsonSerializer>();
            var converter = new HalEmbeddedResourceConverter();
            Assert.Throws<InvalidOperationException>(
                () => converter.WriteJson(fakeWriter.Object, new object(), fakeSerializer.Object));
        }

        [TestCase]
        public void CanWriteEmbeddedResourceJson() {
            var converter = new HalEmbeddedResourceConverter();
            
            var target = new HalEmbeddedResource(new HalEmbeddedResourceBuilder( new TestResource
                {
                    Foo = "bar"
                }));

            var serializer = new JsonSerializer();
            serializer.Converters.Add(new HalDocumentConverter());
            var builder = new StringBuilder();
            using ( var textWriter = new StringWriter( builder ) )
            using ( var writer = new JsonTextWriter( textWriter ) ) {
                converter.WriteJson( writer, target, serializer );
            }

            var actual = builder.ToString();
            string expectedOutput =
                String.Format(
                    "{{\"{0}\":\"{1}\",\"_links\":{{}}}}",
                    "Foo", "bar" );
            Assert.That( actual, Is.EqualTo( expectedOutput ) );
        }
    }

    public class TestResource : IHalResource {
        public string Foo { get; set; }
    }
}
