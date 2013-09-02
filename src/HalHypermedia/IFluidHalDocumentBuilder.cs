using System;
using System.Collections.Generic;
using Hal9000.Json.Net.Fluent;

namespace Hal9000.Json.Net {

    public interface IFluentHalDocumentBuilder : IBuild, IRelationBuilder, IWhen {
    }

}

namespace Hal9000.Json.Net.Fluent {
    
    public interface IBuild {
        HalDocument BuildDocument();
    }

    public interface IWhen {
        IRelationBuilder When(Func<bool> predicate);
    }

    public interface IRelationBuilder : ILinkRelationOperator, IEmbeddedRelationOperator {
    }

    public interface ILinkRelationOperator {
        ILinkOperator WithSelfRelation();
        ILinkOperator WithLinkRelation(string relationValue);
    }

    public interface IEmbeddedRelationOperator {
        IEmbeddedOperator WithEmbeddedRelation(string relationValue);
    }

    public interface IEmbeddedOperator {
        IEmbeddedRelationChoice Having { get; }
    }

    public interface ILinkOperator {
        ILinkChoice Having { get; }
    }

    public interface ILinkJoiner : IBuild {
        IRelationBuilder And { get; }
    }

    public interface IEmbeddedJoiner : ILinkJoiner {
        IEmbeddedResourceChoice Also { get; }
    }

    public interface IResourceOperator {
        IResourceLinkRelationOperator Resource(IHalResource resource);
    }

    public interface IEmbeddedResourceChoice : IResourceLinkRelationOperator, IResourceOperator {
    }

    public interface IResourceLinkRelationOperator {
        IResourceLinkOperator WithSelfRelation();
        IResourceLinkOperator WithLinkRelation(string relationValue);
    }

    public interface IResourceLinkOperator {
        IResourceLinkChoice Having { get; }
    }

    public interface IResourceLinkChoice {
        IEmbeddedJoiner Link(HalLink link);
        IEmbeddedJoiner Links(IEnumerable<HalLink> links);
    }

    public interface IResourcesOperator {
        IResourceOperator MultipleResources { get; }
    }

    public interface IEmbeddedRelationChoice : IResourceOperator, IResourcesOperator {
    }

    public interface ILinkChoice {
        ILinkJoiner Link(HalLink link);
        ILinkJoiner Links(IEnumerable<HalLink> links);
    }
}
