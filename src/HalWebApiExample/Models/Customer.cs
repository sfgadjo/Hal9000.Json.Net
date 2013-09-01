using Hal9000.Json.Net;

namespace HalWebApiExample.Models {
    public class Customer : IHalResource {

        public string Name { get; set; }
        public string Title { get; set; }
    }
}