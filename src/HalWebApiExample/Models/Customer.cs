using Hal9000.Json.Net;
using Newtonsoft.Json;

namespace HalWebApiExample.Models {
    public class Customer : IHalResource {

        public string Name { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public string SocialSecurityNumber { get; set; }
    }
}