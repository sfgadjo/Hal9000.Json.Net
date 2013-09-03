using System;

namespace Hal9000.Json.Net.Fluent {

    /// <summary>
    /// Defines behavior for a predicate when embedding a resource.
    /// </summary>
    public interface IEmbeddedCriteria {

        /// <summary>
        /// Adds a predicate to the composition of the embedded resource.
        /// </summary>
        /// <param name="predicate">A precondition or criteria that must be met in order for the composition of the embedded resource to occur.</param>
        IEmbeddedHaving When(Func<bool> predicate);
    }
}