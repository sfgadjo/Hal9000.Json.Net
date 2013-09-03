namespace Hal9000.Json.Net.Fluent {

    /// <summary>
    /// Defines the behavior for composition of links on a resource.
    /// </summary>
    public interface IResourceLinkRelationOperator {

        /// <summary>
        /// Compose a 'self' link on the resource.
        /// </summary>
        /// <returns>Additional composition options.</returns>
        IResourceLinkOperator WithSelfRelation();

        /// <summary>
        /// Compose a link on the resource.
        /// </summary>
        /// <param name="relationValue">The relation of the link to the resource.</param>
        /// <returns>Additional composition options.</returns>
        IResourceLinkOperator WithLinkRelation ( string relationValue );
    }
}