using Newtonsoft.Json;
using petsitterbackend.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace petsitterbackend.Entities
{
    //tramite questa classe si può serializzare e deserializzare json in un oggetto e viceversa per usarli nel backend
    //string json = JsonConvert.SerializeObject(jsonObject);
    //BaseEntity deserializedJson = JsonConvert.DeserializeObject<BaseEntity>(json);
    public class BaseEntity
    {
        public virtual BaseEntity deserialize(string jsonToDeserialize)
        {
            BaseEntity toSender = null;
            try
            {
                toSender = JsonUtilities.deserializeObject<BaseEntity>(jsonToDeserialize);
            }
            catch (Exception ex)
            { }
            return toSender;
        }
        public virtual BaseEntity deserialize(object jsonToDeserialize)
        {
            BaseEntity toSender = null;
            try
            {
                toSender = deserialize(serialize(jsonToDeserialize));
            }
            catch (Exception ex)
            { }
            return toSender;
        }
        public virtual string serialize(object objectToSerialize)
        {
            string toSender = string.Empty;
            try
            {
                toSender = JsonUtilities.serializeObject(objectToSerialize);
            }
            catch (Exception ex)
            { }
            return toSender;
        }
    }
}