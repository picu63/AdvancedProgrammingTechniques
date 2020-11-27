using System;
using System.Collections.Generic;

namespace CarLibrary
{
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Generation { get; set; }
        public int ProductionYear { get; set; }
        public Dictionary<string, string> LanguageDictionary { get; set; }
        public override string ToString()
        {
            return Brand + " " + Model + " " + Generation + " " + ProductionYear;
        }
    }
}
