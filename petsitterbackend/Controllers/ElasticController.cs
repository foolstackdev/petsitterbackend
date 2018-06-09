using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Web;
using System.Collections.Specialized;
using petsitterbackend.Common;

namespace petsitterbackend.Controllers
{
    //con una classe di questo tipo si può definire un oggetto specifico
    //da usare nelle query
    [DataContract]
    public class ExampleClass
    {
        [DataMember]
        public string name;
        [DataMember]
        public string surname;
        [DataMember]
        public List<Data> data;

        [DataContract]
        public class Data
        {
            [DataMember]
            public string address;
            [DataMember]
            public string city;
        }
    }
    public class ElasticController : ApiController
    {
        //GET api/elastic?index=value
        //GET api/elastic?index=value&type=value
        //GET api/elastic?index=value&type=value&id=value
        public HttpResponseMessage Get()
        {
            string[] queryParameters = new string[3];
            string jsonString = string.Empty;
            object deserializedJson = null;
            HttpResponseMessage toSender = null;
            RESTUtilities.getQueryParameters(HttpContext.Current.Request.QueryString, ref queryParameters, Constants.RESTAPIs.RESTMethods.GET);
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ElasticSearchUtilities.getQueryForElasticSearch(Constants.RESTAPIs.RESTMethods.GET, queryParameters));
                using (HttpWebResponse elasticResponse = (HttpWebResponse)request.GetResponse())
                using (Stream stream = elasticResponse.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                    jsonString = reader.ReadToEnd();
                deserializedJson = JsonConvert.DeserializeObject(jsonString);
                toSender = Request.CreateResponse(Constants.RESTAPIs.RESTMessages.Status200OK.StatusCode, deserializedJson);
            }
            catch (Exception ex)
            {
                toSender = Request.CreateResponse(Constants.RESTAPIs.RESTMessages.Status500InternalServerError.StatusCode, LogUtilities.formatException(ex));
            }
            return toSender;
        }

        //si può mandare sia un oggetto specifico tramite 
        //public HttpResponseMessage Post(string elasticQuery, ExampleClass body)
        //che un oggetto qualsiasi tramite
        //public HttpResponseMessage Post(string elasticQuery, object body)

        //POST api/elastic?index=value&type=value + json body qualsiasi
        public HttpResponseMessage Post(object body)
        {
            string[] queryParameters = new string[2];
            HttpResponseMessage toSender;
            RESTUtilities.getQueryParameters(HttpContext.Current.Request.QueryString, ref queryParameters, Constants.RESTAPIs.RESTMethods.POST);
            try
            {
                string jsonString = JsonConvert.SerializeObject(body);
                byte[] dataBytes = Encoding.ASCII.GetBytes(jsonString);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ElasticSearchUtilities.getQueryForElasticSearch(Constants.RESTAPIs.RESTMethods.POST, queryParameters, body));
                request.Method = Constants.RESTAPIs.RESTMethods.POST.ToString();
                request.ContentType = Constants.RESTAPIs.RESTContentTypes.applicationJson;
                request.ContentLength = jsonString.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(dataBytes, 0, jsonString.Length);
                    stream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                toSender = Request.CreateResponse(response.StatusCode, RESTUtilities.getAndFormatRESTResponse(response));
            }
            catch (WebException ex)
            {
                toSender = Request.CreateResponse(Constants.RESTAPIs.RESTMessages.Status500InternalServerError.StatusCode, LogUtilities.formatWebException(ex));
            }
            catch (Exception ex)
            {
                toSender = Request.CreateResponse(Constants.RESTAPIs.RESTMessages.Status500InternalServerError.StatusCode, LogUtilities.formatException(ex));
            }
            return toSender;
        }

        //DELETE api/elastic?index=value
        //DELETE api/elastic?index=value&type=value
        //DELETE api/elastic?index=value&type=value&id=value
        public HttpResponseMessage Delete(object body)
        {
            string[] queryParameters = new string[2];
            HttpResponseMessage toSender;
            RESTUtilities.getQueryParameters(HttpContext.Current.Request.QueryString, ref queryParameters, Constants.RESTAPIs.RESTMethods.DELETE);
            try
            {
                string jsonString = JsonConvert.SerializeObject(body);
                byte[] dataBytes = Encoding.ASCII.GetBytes(jsonString);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ElasticSearchUtilities.getQueryForElasticSearch(Constants.RESTAPIs.RESTMethods.DELETE, queryParameters, body));
                request.Method = Constants.RESTAPIs.RESTMethods.DELETE.ToString();
                request.ContentType = Constants.RESTAPIs.RESTContentTypes.applicationJson;
                request.ContentLength = jsonString.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(dataBytes, 0, jsonString.Length);
                    stream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                toSender = Request.CreateResponse(response.StatusCode, RESTUtilities.getAndFormatRESTResponse(response));
            }
            catch (WebException ex)
            {
                toSender = Request.CreateResponse(Constants.RESTAPIs.RESTMessages.Status500InternalServerError.StatusCode, LogUtilities.formatWebException(ex));
            }
            catch (Exception ex)
            {
                toSender = Request.CreateResponse(Constants.RESTAPIs.RESTMessages.Status500InternalServerError.StatusCode, LogUtilities.formatException(ex));
            }
            return toSender;
        }

        //PUT api/elastic?queryScope=index.document + json body qualsiasi
        public HttpResponseMessage Put(object body)
        {
            string[] queryParameters = new string[2];
            HttpResponseMessage toSender;
            RESTUtilities.getQueryParameters(HttpContext.Current.Request.QueryString, ref queryParameters, Constants.RESTAPIs.RESTMethods.PUT);
            try
            {
                string jsonString = JsonConvert.SerializeObject(body);
                byte[] dataBytes = Encoding.ASCII.GetBytes(jsonString);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ElasticSearchUtilities.getQueryForElasticSearch(Constants.RESTAPIs.RESTMethods.PUT, queryParameters));
                request.Method = Constants.RESTAPIs.RESTMethods.PUT.ToString();
                request.ContentType = Constants.RESTAPIs.RESTContentTypes.applicationJson;
                request.ContentLength = jsonString.Length;
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(dataBytes, 0, jsonString.Length);
                    stream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                toSender = Request.CreateResponse(response.StatusCode, RESTUtilities.getAndFormatRESTResponse(response));
            }
            catch (WebException ex)
            {
                toSender = Request.CreateResponse(Constants.RESTAPIs.RESTMessages.Status500InternalServerError.StatusCode, LogUtilities.formatWebException(ex));
            }
            catch (Exception ex)
            {
                toSender = Request.CreateResponse(Constants.RESTAPIs.RESTMessages.Status500InternalServerError.StatusCode, LogUtilities.formatException(ex));
            }
            return toSender;
        }
    }
}