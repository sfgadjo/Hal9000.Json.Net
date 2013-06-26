using System.Collections.Generic;

namespace HalHypermedia {
    public interface IHalEmbeddedResourceBuilder {
        IHalEmbeddedResourceBuilder IncludeRelationWithSingleLink ( HalRelation relation, HalLink link );
        IHalEmbeddedResourceBuilder IncludeRelationWithMultipleLinks ( HalRelation relation, IEnumerable<HalLink> links );
        HalResource Build ();
    }
}
