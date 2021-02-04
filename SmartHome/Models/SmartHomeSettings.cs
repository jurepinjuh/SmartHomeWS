using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class SmartHomeSettings
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("power")]
        public bool Power {get; set; }

        [BsonElement("interval")]
        public int Interval { get; set; }

        [BsonElement("workingFrom")]
        public int WorkingFrom { get; set; }
        [BsonElement("workingTo")]
        public int WorkingTo { get; set; }
    }
}
