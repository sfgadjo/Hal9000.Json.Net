namespace Hal9000.Json.Net.Fluent {
    public interface ILinkRelationOperator {
        ILinkOperator WithSelfRelation();
        ILinkOperator WithLinkRelation(string relationValue);
    }
}