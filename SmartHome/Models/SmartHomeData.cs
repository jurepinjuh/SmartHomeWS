using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Models
{
    public class SmartHomeData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("temperature")]
        public double Temperature { get; set; }
        [BsonElement("humidity")]
        public double Humidity { get; set; }
        [BsonElement("dateTime")]
        
        public DateTime dateTime {get; set; }
    }
}
