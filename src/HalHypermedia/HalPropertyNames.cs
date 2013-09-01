namespace Hal9000.Json.Net {

    /// <summary>
    /// Property names that follow the HAL conventions.
    /// </summary>
    internal static class HalPropertyNames {

        /// <summary>
        /// The property name for the links collection.
        /// </summary>
        public static readonly string Links = "_links";

        /// <summary>
        /// The property name for the embedded resources collection.
        /// </summary>
        public static readonly string Embedded = "_embedded";
        
        /// <summary>
        /// The property name for a self relation.
        /// </summary>
        public static readonly string Self = "self";
    }
}
