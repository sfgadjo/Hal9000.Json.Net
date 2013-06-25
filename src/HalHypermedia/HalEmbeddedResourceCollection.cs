using System.Collections.Generic;

namespace HalHypermedia {
    public class HalEmbeddedResourceCollection : Dictionary<HalRelation, IEnumerable<HalEmbeddedResourceRepresentation>> {
    }
}