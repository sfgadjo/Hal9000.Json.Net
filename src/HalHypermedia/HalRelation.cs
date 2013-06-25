using System;

namespace HalHypermedia {
    public class HalRelation {

        private const string SELF = "self";

        private readonly string _value;
        private HalRelation(string relation) {
            _value = relation;
        }

        public static HalRelation CreateOrThrow(string relation) {
            if ( String.IsNullOrWhiteSpace( relation ) ) {
                throw new ArgumentException( "relation cannot be null or empty.", "relation" );
            }

            if ( relation.Contains( " " ) ) {
                throw new InvalidOperationException( "relation cannot contain any of the" );
            }
            return new HalRelation(relation);
        }

        public static HalRelation CreateSelfRelation() {
            return new HalRelation( SELF );
        }

        public string Value {
            get { return _value; }
        }
    }
}