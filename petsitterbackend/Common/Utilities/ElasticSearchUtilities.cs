using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using petsitterbackend.Entities;
using System.Collections.Specialized;

namespace petsitterbackend.Common
{
    public static class ElasticSearchUtilities
    {
        public static string getQueryForElasticSearch(Constants.RESTAPIs.RESTMethods RESTMethod, string[] queryParameters, object body = null)
        {
            string toSender = string.Empty;
            try
            {
                string index = queryParameters[0];
                string type = queryParameters[1];
                string id = string.Empty;
                if (queryParameters.Length == 3)
                    id = queryParameters[2];
                index = string.IsNullOrEmpty(index) ? index : "/" + index;
                type = string.IsNullOrEmpty(type) ? type : "/" + type;
                id = string.IsNullOrEmpty(id) ? id : "/" + id;

                if (RESTMethod.Equals(Constants.RESTAPIs.RESTMethods.GET))
                {
                    toSender = string.Format(Constants.ElasticSearch.UrlsAndPaths.elasticUrl + "{0}" + "{1}" + "{2}", index, type, id);
                    if (string.IsNullOrEmpty(index) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(id))
                        toSender = toSender + Constants.ElasticSearch.UrlFormats.search;
                    if (string.IsNullOrEmpty(index) && (!string.IsNullOrEmpty(type) || !string.IsNullOrEmpty(id)))
                        toSender = Constants.ElasticSearch.UrlsAndPaths.elasticUrl + Constants.ElasticSearch.UrlFormats.search;
                }
                else if (RESTMethod.Equals(Constants.RESTAPIs.RESTMethods.POST))
                {
                    toSender = string.Format(Constants.ElasticSearch.UrlsAndPaths.elasticUrl + "{0}" + "{1}", index, type);
                    //invalid request, a post request needs both the index and type
                    if (string.IsNullOrEmpty(index) && !string.IsNullOrEmpty(type))
                        toSender = string.Empty;
                }
                else if (RESTMethod.Equals(Constants.RESTAPIs.RESTMethods.DELETE))
                {
                    //qui passo il body per estrarne l'id dell'item da cancellare
                    if (body != null)
                    {
                        SimpleEntity simpleEntity = new SimpleEntity();
                        simpleEntity = (SimpleEntity)simpleEntity.deserialize(body);
                        toSender = string.Format(Constants.ElasticSearch.UrlsAndPaths.elasticUrl + "{0}" + "{1}", index, type) + "/" + simpleEntity.id;
                    }
                    else if (body == null)
                    {
                        toSender = string.Format(Constants.ElasticSearch.UrlsAndPaths.elasticUrl + "{0}" + "{1}", index, type);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtilities.formatException(ex);
            }
            return toSender;
        }
    }
}