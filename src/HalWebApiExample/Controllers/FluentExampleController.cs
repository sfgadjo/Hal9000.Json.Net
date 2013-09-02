using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hal9000.Json.Net;
using Hal9000.Json.Net.MediaTypeFormatters;
using HalWebApiExample.Models;

namespace HalWebApiExample.Controllers
{
    public class FluentExampleController : ApiController
    {
        public HttpResponseMessage Get(HttpRequestMessage request, bool? predicate )
        {
            if (!predicate.HasValue) {
                predicate = true;
            }
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

            var builder = new FluentHalDocumentBuilder(orderLog);

            HalDocument document =
                builder
                    .WithSelfRelation().When( () => predicate.Value )
                       .Having.Link(new HalLink("/orderlogs/123"))
                    .And
                        .WithLinkRelation("archive")
                            .Having.Links(new[]
                               {
                                   new HalLink("/orderlogs/archive/") {Profile = "http://profiles.mydomain.com/standard/"},
                                   new HalLink("m/orderlogs/archive") {Profile = "http://profiles.mydomain.com/mobile/"}
                               })
                       .And
                            .WithEmbeddedRelation( "customer" ).When( () => predicate.Value )
                            .Having.Resource(customer)
                                .WithSelfRelation()
                                    .Having.Link(new HalLink("/customer/112")
                                           {
                                               Profile =
                                                   "https://profiles.mydomain.com/customer/"
                                           })
                                .Also
                                    .WithLinkRelation("tags")
                                        .Having.Links(new[] {new HalLink("/tags/123"), new HalLink("tags/345")})
                            .And
                                .WithEmbeddedRelation("contrived")
                                    .Having
                                        .MultipleResources
                                            .Resource(customer)
                                                .WithSelfRelation()
                                                    .Having.Link( new HalLink( "/customer/112" ) {
                                                        Profile =
                                                            "https://profiles.mydomain.com/customer/"
                                                    } )
                                             .Also
                                                .WithLinkRelation( "tags" )
                                                    .Having.Links( new[] { new HalLink( "/tags/123" ), new HalLink( "tags/345" ) } )
                                        .Also
                                            .Resource( customer )
                                                .WithSelfRelation()
                                                    .Having.Link( new HalLink( "/customer/112" ) {
                                                        Profile =
                                                            "https://profiles.mydomain.com/customer/"
                                                    } )
                                             .Also
                                                .WithLinkRelation( "tags" )
                                                    .Having.Links( new[] { new HalLink( "/tags/123" ), new HalLink( "tags/345" ) } )
               .BuildDocument();
            
 

            //Create a response that will have our HAL document. We use the appropriate media type on the response.
            //The HalDocument we built will get serialized into HAL formatted JSON since we added the HalJsonMediaTypeFormatter
            //to the application's formatters collection during application start up in the WebApiConfig.cs file.
            var response = request.CreateResponse(HttpStatusCode.OK, document,
                                                  HalJsonMediaTypeFormatter.SupportedMediaType);

            return response;
        }

    }
}
