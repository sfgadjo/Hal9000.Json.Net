using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using HalHypermedia;

namespace HALPrototype.Controllers {
    public class ValuesController : ApiController {
        // GET api/values
        public object Get () {
           
            var msg = new HttpResponseMessage();
            var bldr = new HalResourceBuilder(new Customer { Name = "Joe Blow",Title = "President"});
            bldr.IncludeRelationWithSingleLink(HalRelation.CreateSelfRelation(), new HalLink {Href = "/something/123"});
            bldr.IncludeRelationWithMultipleLinks( new HalRelation( "customer" ),
                                                  new HalLink[]
                                                      {
                                                          new HalLink {Href = "/customer/1"},
                                                          new HalLink {Href = "/customer/2"}
                                                      });

            var embeddedBldr = new HalEmbeddedResourceBuilder(new Customer
                {
                    Name = "Sharon Jones",
                    Title = "CEO"
                }).IncludeRelationWithSingleLink(new HalRelation("product"), new HalLink
                        {
                            Href = "/new/112",
                            Profile = "https://profiles.com"
                        });
            var embeddedResource = new HalEmbeddedResource(embeddedBldr);
            bldr.IncludeEmbeddedWithSingleResource( new HalRelation( "order" ), embeddedResource );

            var embeds = new List<HalEmbeddedResource>
                {
                    new HalEmbeddedResource(embeddedBldr),
                    new HalEmbeddedResource(embeddedBldr)
                };

            bldr.IncludeEmbeddedWithMultipleResources( new HalRelation( "product" ),
                                                      embeds);
            var resource = bldr.Build();
            
            //use the formatter added in the WebApiConfig
            msg.Content = new ObjectContent( typeof( HalResource ), resource, new MediaTypeFormatterCollection().JsonFormatter, "application/hal+json" );

            return msg;
        }

        // GET api/values/5
        public string Get ( int id ) {
            return "value";
        }

        // POST api/values
        public void Post ( [FromBody]string value ) {
        }

        // PUT api/values/5
        public void Put ( int id, [FromBody]string value ) {
        }

        // DELETE api/values/5
        public void Delete ( int id ) {
        }

    }
}