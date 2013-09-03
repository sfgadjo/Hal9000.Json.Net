namespace Hal9000.Json.Net.Fluent {

    /// <summary>
    /// Defines behavior for building a <see cref="HalDocument"/>.
    /// </summary>
    public interface IBuild {
        /// <summary>
        /// Builds a <see cref="HalDocument"/>.
        /// </summary>
        /// <returns>A <see cref="HalDocument"/>.</returns>
        HalDocument BuildDocument();
    }
}