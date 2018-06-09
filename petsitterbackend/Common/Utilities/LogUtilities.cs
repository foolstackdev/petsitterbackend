using Newtonsoft.Json;
using petsitterbackend.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace petsitterbackend.Common
{
    public static class LogUtilities
    {
        public static string formatException(Exception exception)
        {
            string formattedError = string.Empty;
            try
            {
                formattedError = exception.Message + " " + exception.StackTrace;
                logErrorOnFile(formattedError);
                logErrorOnElastic(formattedError);
            }
            catch (Exception ex)
            {
                logErrorOnElastic(ex.Message + " " + ex.StackTrace);
            }
            return formattedError;
        }
        public static string formatWebException(WebException webException)
        {
            string formattedError = string.Empty;
            try
            {
                string responseError = RESTUtilities.getResponseError(webException);
                formattedError = webException.Message + " " + webException.StackTrace + " " + responseError;
                logErrorOnFile(formattedError);
                logErrorOnElastic(formattedError);
            }
            catch (Exception ex)
            {
                logErrorOnElastic(ex.Message + " " + ex.StackTrace);
            }
            return formattedError;
        }
        private static void logErrorOnFile(string error)
        {
            try
            {
                StringBuilder logger = new StringBuilder();
                logger.AppendLine(error);
                File.AppendAllText(Constants.LogManager.LogFile.path, logger.ToString());
            }
            catch (Exception ex)
            {
                logErrorOnElastic(ex.Message + " " + ex.StackTrace);
            }
        }
        public static void logErrorOnElastic(string error)
        {
            try
            {
                ErrorEntity errorEntity = new ErrorEntity(error, DateTime.Now);
                string jsonString = errorEntity.serialize(errorEntity);
                byte[] dataBytes = Encoding.ASCII.GetBytes(jsonString);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ElasticSearchUtilities.getQueryForElasticSearch(Constants.RESTAPIs.RESTMethods.POST, new string[] { Constants.LogManager.ElasticSearchLogs.index, Constants.LogManager.ElasticSearchLogs.Errors.type }, null));
                request.Method = Constants.RESTAPIs.RESTMethods.POST.ToString();
                request.ContentType = Constants.RESTAPIs.RESTContentTypes.applicationJson;
                request.ContentLength = jsonString.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(dataBytes, 0, jsonString.Length);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //not much else to do here
            }
        }
    }
}