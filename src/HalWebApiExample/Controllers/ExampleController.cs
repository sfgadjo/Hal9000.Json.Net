using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using HalWebApiExample.Models;
using Hal9000.Json.Net;
using Hal9000.Json.Net.MediaTypeFormatters;

namespace HalWebApiExample.Controllers {

    /// <summary>
    /// A contrived example to demonstrate using the Hal9000.Json.Net library.
    /// </summary>
    [HandleError]
    public class ExampleController : ApiController {

        /// <summary>
        /// Gets a customer with HAL formatted hypermedia.
        /// </summary>
        public HttpResponseMessage Get(HttpRequestMessage request) {

            //this will be our embedded resource.
            var customer = new Customer
                {
                    Name = "Joe Blow",
                    Title = "President"
                };

            //this will be our root resource
            var orderLog = new OrderLog
                {
                    NumberOfItemsPurchase = 2,
                    OrderDate = new DateTime(1999, 12, 31),
                    TotalAmount = 43.23m
                };

            //Create a builder so we can build our HAL document that will get serialized.
            var bldr = new HalDocumentBuilder(orderLog);

            //Add all of our links to our HAL document.
            //Note: The use of relative paths for brevity.
            bldr.IncludeRelationWithSingleLink(HalRelation.CreateSelfRelation(), new HalLink("/orderlogs/123"));
            bldr.IncludeRelationWithMultipleLinks(new HalRelation("archive"),
                                                  new[]
                                                      {
                                                          new HalLink("/orderlogs/archive/") { Profile = "http://profiles.mydomain.com/standard/"},
                                                          new HalLink("m/orderlogs/archive") { Profile = "http://profiles.mydomain.com/mobile/"}
                                                      });

            //Create a builder to build our Customer embedded resource.
            var embeddedBldr =
                new HalEmbeddedResourceBuilder(customer);

            //Add all of our HAL links that will be on the Customer embedded resource.
            embeddedBldr
                .IncludeRelationWithSingleLink( HalRelation.CreateSelfRelation(),
                                               new HalLink("/customer/112")
                                                   {
                                                       Profile =
                                                           "https://profiles.mydomain.com/customer/"
                                                   })
                .IncludeRelationWithMultipleLinks(new HalRelation("tags"),
                                                  new[] {new HalLink("/tags/123"), new HalLink("tags/345")});

            //Create an embedded resource with the links we added through the builder.
            var embeddedResource = new HalEmbeddedResource(embeddedBldr);

            //Add our single embedded resource to our HAL document builder.
            bldr.IncludeEmbeddedWithSingleResource(new HalRelation("customer"), embeddedResource);


            var embeds = new List<HalEmbeddedResource>
                {
                    new HalEmbeddedResource(embeddedBldr),
                    new HalEmbeddedResource(embeddedBldr)
                };

            //Add a relation that has 2 embedded resources.
            bldr.IncludeEmbeddedWithMultipleResources(new HalRelation("contrived"),
                                                      embeds);

            //Build our HAL document.
            HalDocument document = bldr.Build();

            //Create a response that will have our HAL document. We use the appropriate media type on the response.
            //The HalDocument we built will get serialized into HAL formatted JSON since we added the HalJsonMediaTypeFormatter
            //to the application's formatters collection during application start up in the WebApiConfig.cs file.
            var response = request.CreateResponse(HttpStatusCode.OK, document,
                                                  HalJsonMediaTypeFormatter.SupportedMediaType);

            return response;
        }
    }
}