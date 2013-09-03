using System;

namespace Hal9000.Json.Net.Fluent {
    public interface IResourceCriteria {
        IResourceHaving When ( Func<bool> predicate );
    }
}