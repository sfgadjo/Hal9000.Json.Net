using System;

namespace Hal9000.Json.Net {

    /// <summary>
    /// Represents the relationship between the link and the resource.
    /// </summary>
    public sealed class HalRelation : IEquatable<HalRelation>
    {
        private readonly string _value;

        /// <summary>
        /// Creates an instance of <see cref="HalRelation"/>.
        /// </summary>
        /// <param name="relation"></param>
        public HalRelation(string relation) {
            if ( String.IsNullOrWhiteSpace( relation ) ) {
                throw new ArgumentException( "relation cannot be null or empty.", "relation" );
            }

            if ( relation.Contains( " " ) ) {
                throw new InvalidOperationException( "relation cannot contain any of the" );
            }
            _value = relation;
        }

        /// <summary>
        /// Convenience method for creating a 'self' relation.
        /// </summary>
        /// <returns>A <see cref="HalRelation"/> intialized with the value that follows HAL convention for a self relation.</returns>
        public static HalRelation CreateSelfRelation() {
            return new HalRelation( HalPropertyNames.Self );
        }

        /// <summary>
        /// Gets the relation's value.
        /// </summary>
        public string Value {
            get { return _value; }
        }

        /// <summary>
        /// Returns true if the target instance and this instance are equal.
        /// </summary>
        /// <param name="other">The target object to test for equality.S</param>
        /// <returns>True if the target instance and this instance are equal.</returns>
        public bool Equals(HalRelation other)
        {
            bool result = Equals((object)other);
            return result;
        }

        /// <summary>
        /// Returns true if the target instance and this instance are equal.
        /// </summary>
        /// <param name="other">The target object to test for equality.S</param>
        /// <returns>True if the target instance and this instance are equal.</returns>
        public override bool Equals ( object other )
        {
            var obj = other as HalRelation;

            if ( obj == null )
            {
                return false;
            }

            if ( ReferenceEquals( obj, this ) )
            {
                return true;
            }
            if ( String.CompareOrdinal( obj.Value, _value ) == 0 )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the hash code for this object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}