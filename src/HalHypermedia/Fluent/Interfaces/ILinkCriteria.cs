using System;

namespace Hal9000.Json.Net.Fluent {
    public interface ILinkCriteria {
        ILinkHaving When ( Func<bool> predicate );
    }
}