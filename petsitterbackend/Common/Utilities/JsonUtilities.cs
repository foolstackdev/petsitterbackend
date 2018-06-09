using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace petsitterbackend.Common
{
    public static class JsonUtilities
    {
        //string json = JsonConvert.SerializeObject(jsonObject);
        //BaseEntity deserializedJson = JsonConvert.DeserializeObject<BaseEntity>(json);
        public static string serializeObject(object objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }
        public static T deserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}