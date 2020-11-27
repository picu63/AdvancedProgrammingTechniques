using System;
using System.Collections.Generic;
using System.Text;

namespace MongoFeeder.Models
{
    public class Car
    {
        public int ID { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public int ProductionYear { get; set; }
        public Dictionary<string, string> Languages { get; set; } = new Dictionary<string, string>();
        
    }
}
