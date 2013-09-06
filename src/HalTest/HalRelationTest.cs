using System;
using Hal9000.Json.Net;
using NUnit.Framework;

namespace HalTest {

    [TestFixture]
    public class HalRelationTest {
        
        [TestCase]
        public void ConstructingRelationWhenValueIsOnlySpaceThrowsException() {
            Assert.Throws<ArgumentException>(() => new HalRelation(" "));
        }
    }
}
