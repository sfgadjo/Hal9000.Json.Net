using System.Collections.Generic;

namespace Hal9000.Json.Net.Fluent {
    public interface IResourceLinkChoice {
        IEmbeddedJoiner Link(HalLink link);
        IEmbeddedJoiner Links(IEnumerable<HalLink> links);
    }
}