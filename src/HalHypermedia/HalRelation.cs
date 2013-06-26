using System;

namespace Hal9000.Json.Net {
    public sealed class HalRelation : IEquatable<HalRelation>
    {

        private const string SELF = "self";

        private readonly string _value;
        public HalRelation(string relation) {
            if ( String.IsNullOrWhiteSpace( relation ) ) {
                throw new ArgumentException( "relation cannot be null or empty.", "relation" );
            }

            if ( relation.Contains( " " ) ) {
                throw new InvalidOperationException( "relation cannot contain any of the" );
            }
            _value = relation;
        }

        public static HalRelation CreateSelfRelation() {
            return new HalRelation( SELF );
        }

        public string Value {
            get { return _value; }
        }

        public bool Equals(HalRelation other)
        {
            bool result = Equals((object)other);
            return result;
        }

        public override bool Equals(object obj)
        {
            var other = obj as HalRelation;

            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(other, this))
            {
                return true;
            }
            if (String.CompareOrdinal(other.Value, _value) == 0)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}