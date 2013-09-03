namespace Hal9000.Json.Net.Fluent {

    /// <summary>
    /// Defines behavior for composing an embedded resource relation.
    /// </summary>
    public interface IEmbeddedRelationOperator {

        /// <summary>
        /// Compose an embedded resource relation on the resource.
        /// </summary>
        /// <param name="relationValue">The relation of the embedded resource to the resource.</param>
        /// <returns>Additional composition options.</returns>
        IEmbeddedOperator WithEmbeddedRelation(string relationValue);
    }
}