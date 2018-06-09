using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace petsitterbackend.Common
{
    public static class RESTUtilities
    {
        public static void getQueryParameters(NameValueCollection queryString, ref string[] queryParameters, Constants.RESTAPIs.RESTMethods RESTMethod)
        {
            try
            {
                NameValueCollection values = HttpContext.Current.Request.QueryString;
                if (RESTMethod.Equals(Constants.RESTAPIs.RESTMethods.GET))
                {
                    foreach (string key in values)
                    {
                        if (key.Equals(Constants.ElasticSearch.UrlParameters.index))
                            queryParameters[0] = values[key];
                        else if (key.Equals(Constants.ElasticSearch.UrlParameters.type))
                            queryParameters[1] = values[key];
                        else if (key.Equals(Constants.ElasticSearch.UrlParameters.id))
                            queryParameters[2] = values[key];
                    }
                }
                //per le post lasciamo che elastic gestisca l'id automatico
                //nelle delete l'id viene passato nel body del json
                if (RESTMethod.Equals(Constants.RESTAPIs.RESTMethods.POST) || RESTMethod.Equals(Constants.RESTAPIs.RESTMethods.DELETE))
                {
                    foreach (string key in values)
                    {
                        if (key.Equals(Constants.ElasticSearch.UrlParameters.index))
                            queryParameters[0] = values[key];
                        else if (key.Equals(Constants.ElasticSearch.UrlParameters.type))
                            queryParameters[1] = values[key];
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtilities.formatException(ex);
            }
        }
        public static string getAndFormatRESTResponse(HttpWebResponse response)
        {
            string toSender = string.Empty;
            try
            {
                toSender = (int)response.StatusCode + " " + response.StatusDescription; ;
            }
            catch (Exception ex)
            {
                LogUtilities.formatException(ex);
            }
            return toSender;
        }
        public static string getResponseError(WebException webException)
        {
            string toSender = string.Empty;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)webException.Response)
                using (Stream data = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(data))
                {
                    toSender = reader.ReadToEnd();
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