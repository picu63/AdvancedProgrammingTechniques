using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace ClassLibrary.Models
{
    public class Car
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public Dictionary<string, string> Languages { get; set; } = new Dictionary<string, string>();
        
    }
}
