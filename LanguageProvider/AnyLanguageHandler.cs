using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageProvider
{
    public class AnyLanguageHandler : LanguageHandler
    {
        public override object Handle(Dictionary<string, string> dictionary)
        {
            if (dictionary.Any())
            {
                var firstPair = dictionary.FirstOrDefault();
                var value = firstPair.Value;
                var key = firstPair.Key;
                
                return new KeyValuePair<string, string>(key,value);
            }
            else
            {
                return null;
            }
        }
    }
}
