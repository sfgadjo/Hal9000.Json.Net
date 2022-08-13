using System;
using NUnit.Framework;

namespace Hal9000.Json.Net.Test {

    [TestFixture]
    public class HalRelationTest {
        
        [TestCase]
        public void ConstructingRelationWhenValueIsOnlySpaceThrowsException() {
            Assert.Throws<ArgumentException>(() => new HalRelation(" "));
        }
    }
}
