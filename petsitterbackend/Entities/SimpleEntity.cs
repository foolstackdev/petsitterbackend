using petsitterbackend.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace petsitterbackend.Entities
{
    public class SimpleEntity : BaseEntity
    {
        public string id { get; set; }
        public override BaseEntity deserialize(string json)
        {
            SimpleEntity toSender = null;
            try
            {
                toSender = JsonUtilities.deserializeObject<SimpleEntity>(json);
            }
            catch (Exception ex) { }
            return toSender;
        }
        public override BaseEntity deserialize(object json)
        {
            SimpleEntity toSender = null;
            try
            {
                toSender = (SimpleEntity)deserialize(base.serialize(json));
            }
            catch (Exception ex)
            { }
            return toSender;
        }
    }
}