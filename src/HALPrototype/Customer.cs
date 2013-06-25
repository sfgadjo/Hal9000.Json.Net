using HalHypermedia;

namespace HALPrototype {
    public class Customer : IHalResource {

        public string Name { get; set; }
        public string Title { get; set; }
    }
}