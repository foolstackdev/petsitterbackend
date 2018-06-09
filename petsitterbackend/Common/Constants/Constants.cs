using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace petsitterbackend.Common
{
    public static class Constants
    {
        public struct RESTAPIs
        {
            public struct RESTContentTypes
            {
                public const string applicationJson = "application/json";
            }
            public enum RESTMethods
            {
                POST,
                GET,
                DELETE,
                PUT
            }
            public struct RESTMessages
            {
                public struct Status200OK
                {
                    public const int Code = 200;
                    public const string Status = "OK";
                    public const HttpStatusCode StatusCode = HttpStatusCode.OK;
                }
                public struct Status201Created
                {
                    public const int ID = 201;
                    public const string Status = "Created";
                    public const HttpStatusCode StatusCode = HttpStatusCode.Created;
                }
                public struct Status400BadRequest
                {
                    public const int ID = 400;
                    public const string Status = "Bad Request";
                    public const HttpStatusCode StatusCode = HttpStatusCode.BadRequest;
                }
                public struct Status500InternalServerError
                {
                    public const int ID = 500;
                    public const string Status = "Internal Server Error";
                    public const HttpStatusCode StatusCode = HttpStatusCode.InternalServerError;
                }
            }
        }
        public struct LogManager
        {
            public struct LogFile
            {
                public const string path = "C:\\Progetti\\Visual Studio\\ServerBackEnd\\ServerBackEnd\\Logs\\Log.txt";
                //public const string path = "C:\\inetpub\\sites\\barbarella_foolstack_it\\Logs\\Log.txt";
            }
            public struct ElasticSearchLogs
            {
                public const string index = ElasticSearch.IndexesMapping.Logs.index;
                public struct Errors
                {
                    public const string type = ElasticSearch.IndexesMapping.Logs.Errors.type;
                }
                public struct Infos
                {
                    public const string type = ElasticSearch.IndexesMapping.Logs.Infos.type;
                }
            }
        }

        public struct UrlsAndPaths
        {

        }
        public struct ElasticSearch
        {
            public struct UrlsAndPaths
            {
                public const string elasticUrl = @"http://localhost:9200";
            }
            public struct UrlFormats
            {
                public const string search = "/_search";

            }
            public struct UrlParameters
            {
                public const string index = "index";
                public const string type = "type";
                public const string id = "id";
            }
            public struct IndexesMapping
            {
                public struct Logs
                {
                    public const string index = "logs";
                    public struct Errors
                    {
                        public const string type = "error";
                    }
                    public struct Infos
                    {
                        public const string type = "info";
                    }
                }
            }
        }
    }
}