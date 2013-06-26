using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using HalHypermedia;
using HalHypermedia.MediaTypeFormatters;

namespace HALPrototype.Controllers {
    public class ValuesController : ApiController {
        // GET api/values
        public object Get () {
           
            var msg = new HttpResponseMessage();
            var bldr = new HalResourceBuilder(new Customer { Name = "Joe Blow",Title = "President"});
            bldr.IncludeRelationWithSingleLink(HalRelation.CreateSelfRelation(), new HalLink {Href = "/something/123"});
            bldr.IncludeRelationWithMultipleLinks(HalRelation.CreateOrThrow("customer"),
                                                  new HalLink[]
                                                      {
                                                          new HalLink {Href = "/customer/1"},
                                                          new HalLink {Href = "/customer/2"}
                                                      });
            bldr.IncludeEmbeddedWithSingleResource(HalRelation.CreateOrThrow("order"),
                                                   new Customer {Name = "Sharon", Title = "CEO"});
            bldr.IncludeEmbeddedWithMultipleResources(HalRelation.CreateOrThrow("product"),
                                                      new Customer[]
                                                          {new Customer {Name = "Xavier"}, new Customer {Name = "Jose"}});
            var resource = bldr.Build();
            //var rep = new HalRepresentation(new Customer
            //    {
            //        Name = "Joe",
            //        Title = "President"
            //    },
            //                                new HalLinkCollection
            //                                    {
            //                                        {
            //                                            HalRelation.CreateSelfRelation(),
            //                                            new HalLink {Href = "/api/values", Title = "My bad self."}
            //                                        }
            //                                    }, new HalEmbeddedResourceCollection
            //                                        {
            //                                            {
            //                                                HalRelation.CreateOrThrow("customer"),
            //                                                new List<HalEmbeddedResourceRepresentation>{new HalEmbeddedResourceRepresentation(
            //                                           new Customer {Name = "Susan"},
            //                                           new HalLinkCollection
            //                                               {
            //                                                   {
            //                                                       HalRelation.CreateSelfRelation(),
            //                                                       new HalLink {Href = "/customer/123"}
            //                                                   }
            //                                               })
            //                                            }}
            //                                        });
            msg.Content = new ObjectContent( typeof( HalResource ), resource, new HalMediaTypeFormatter(), "application/hal+json" );

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