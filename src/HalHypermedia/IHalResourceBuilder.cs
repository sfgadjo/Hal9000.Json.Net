using System;
using System.Collections.Generic;

namespace HalHypermedia
{
    public interface IHalResourceBuilder
    {
        IHalResourceBuilder IncludeRelationWithSingleLink(HalRelation relation, HalLink link);
        IHalResourceBuilder IncludeRelationWithMultipleLinks(HalRelation relation, IEnumerable<HalLink> links);
        IHalResourceBuilder IncludeEmbeddedWithSingleResource(HalRelation relation, IResource resource);

        IHalResourceBuilder IncludeEmbeddedWithMultipleResources(HalRelation relation,
                                                                   IEnumerable<IResource> resource);

        HalResource Build();
    }
}