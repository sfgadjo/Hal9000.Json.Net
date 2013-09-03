namespace Hal9000.Json.Net.Fluent {

    /// <summary>
    /// Defines behavior for joining the composition of different parts of the embedded resource.
    /// </summary>
    public interface IEmbeddedJoiner : ILinkJoiner {

        /// <summary>
        /// Transitional point of composition for an embedded resource's relation.
        /// </summary>
        IEmbeddedResourceChoice Also { get; }
    }
}