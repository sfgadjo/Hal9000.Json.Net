using System.Collections.Generic;

namespace Hal9000.Json.Net.Fluent {

    /// <summary>
    /// Defines behavior for compositional choices on a link.
    /// </summary>
    public interface ILinkChoice {

        /// <summary>
        /// Composes a link for a resource relation.
        /// </summary>
        /// <param name="link">The link.</param>
        /// <returns>Additional composition choices.</returns>
        ILinkJoiner Link(HalLink link);

        /// <summary>
        /// Composes multiple links for a resource relation.
        /// </summary>
        /// <param name="links">The links.</param>
        /// <returns>Additional composition choices.</returns>
        ILinkJoiner Links ( IEnumerable<HalLink> links );
    }
}