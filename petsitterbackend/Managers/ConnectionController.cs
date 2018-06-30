using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Elasticsearch.Net;
using Nest;
namespace petsitterbackend.Managers
{
    [RoutePrefix("api/connect")]
    public class ConnectionController : ApiController
    {


        [HttpGet]
        [Route("isalive")]
        public string IsAlive()
        {
            return "Server is alive";
        }

        public void nestConnection()
        {
            //var client = new ElasticClient();  http://barbarellaserver.foolstack.it/
            //var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("my-application");
            var uris = new[]
            {
                    new Uri("http://localhost:9200"),
                    new Uri("http://barbarellaserver.foolstack.it/"),
            };
            var connectionPool = new SniffingConnectionPool(uris);

            var settings = new ConnectionSettings(connectionPool).DefaultIndex("people");
            ElasticClient client = new ElasticClient(settings);

            var response = client.ClusterHealth();
            Console.WriteLine(response.Status);
            Console.Read();
            //ElasticClient client = new ElasticClient(settings);


            var person2 = new PersonwithID
            {
                Id = 2,
                FirstName = "Martijn",
                LastName = "Bucky"
            };

            var indexResponse = client.IndexDocument(person2);

            //return client;
        }

        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class PersonwithID
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }


        //public IIndexResponse DatiPersona()
        //{
        //    var person = new PersonwithID
        //    {
        //        Id = 2,
        //        FirstName = "Martijn",
        //        LastName = "Bucky"
        //    };

        //    var indexResponse = nestConnection().IndexDocument(person);
        //    //var asyncIndexResponse = await nestConnection().IndexDocumentAsync(person);
        //    return indexResponse;
        //}

    }





}
