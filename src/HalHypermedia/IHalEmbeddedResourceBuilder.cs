using System.Collections.Generic;

namespace Hal9000.Json.Net {
    public interface IHalEmbeddedResourceBuilder {
        IHalEmbeddedResourceBuilder IncludeRelationWithSingleLink ( HalRelation relation, HalLink link );
        IHalEmbeddedResourceBuilder IncludeRelationWithMultipleLinks ( HalRelation relation, IEnumerable<HalLink> links );
        HalResource Build ();
    }
}
