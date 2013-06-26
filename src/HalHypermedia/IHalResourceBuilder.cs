using System.Collections.Generic;

namespace Hal9000.Json.Net
{
    public interface IHalResourceBuilder {

        IHalResourceBuilder IncludeRelationWithSingleLink(HalRelation relation, HalLink link);
        IHalResourceBuilder IncludeRelationWithMultipleLinks(HalRelation relation, IEnumerable<HalLink> links);

        IHalResourceBuilder IncludeEmbeddedWithSingleResource(HalRelation relation,
                                                              HalEmbeddedResource embeddedResource);

        IHalResourceBuilder IncludeEmbeddedWithMultipleResources(HalRelation relation,
                                                                 IEnumerable<HalEmbeddedResource> embeddedResources);

        HalResource Build();
    }
}