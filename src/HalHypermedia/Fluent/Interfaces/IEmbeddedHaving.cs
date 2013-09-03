namespace Hal9000.Json.Net.Fluent {

    //TODO: come up with a better interface name.
    /// <summary>
    /// Defines behavior for defining the composition attributes of an embedded resource.
    /// </summary>
    public interface IEmbeddedHaving {

        /// <summary>
        /// Transitional point of composition for an embedded resource.
        /// </summary>
        IEmbeddedRelationChoice Having { get; }
    }
}