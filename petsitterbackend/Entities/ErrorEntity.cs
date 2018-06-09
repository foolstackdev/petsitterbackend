using petsitterbackend.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace petsitterbackend.Entities
{
    public class ErrorEntity : BaseEntity
    {
        public string error { get; set; }
        public DateTime dateTime { get; set; }

        public ErrorEntity(string error, DateTime dateTime)
        {
            this.error = error;
            this.dateTime = dateTime;
        }
        public override BaseEntity deserialize(string json)
        {
            ErrorEntity toSender = null;
            try
            {
                toSender = JsonUtilities.deserializeObject<ErrorEntity>(json);
            }
            catch (Exception ex) { }
            return toSender;
        }
        public override BaseEntity deserialize(object json)
        {
            ErrorEntity toSender = null;
            try
            {
                toSender = (ErrorEntity)deserialize(base.serialize(json));
            }
            catch (Exception ex)
            { }
            return toSender;
        }
    }
}