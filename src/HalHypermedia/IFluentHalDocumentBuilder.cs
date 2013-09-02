using System;
using System.Collections.Generic;
using Hal9000.Json.Net.Fluent;

namespace Hal9000.Json.Net {

    public interface IFluentHalDocumentBuilder : IBuild, IRelationBuilder {
    }
}

namespace Hal9000.Json.Net.Fluent {
    
    public interface IBuild {
        HalDocument BuildDocument();
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

    public interface IEmbeddedHaving {
        IEmbeddedRelationChoice Having { get; }
    }

    public interface IEmbeddedCriteria {
        IEmbeddedHaving When(Func<bool> predicate);
    }

    public interface IEmbeddedOperator : IEmbeddedHaving, IEmbeddedCriteria {
    }

    public interface ILinkHaving {
        ILinkChoice Having { get; }
    }

    public interface ILinkCriteria {
        ILinkHaving When ( Func<bool> predicate );
    }

    public interface ILinkOperator : ILinkHaving, ILinkCriteria {
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

    public interface IResourceCriteria {
        IResourceHaving When ( Func<bool> predicate );
    }

    public interface IResourceHaving {
        IResourceLinkChoice Having { get; }
    }

    public interface IResourceLinkOperator : IResourceHaving , IResourceCriteria{
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
