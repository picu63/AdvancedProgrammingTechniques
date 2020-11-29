using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    public class EnglishLanguageHandler : LanguageHandler
    {
        public override object Handle(Dictionary<string,string> dictionary)
        {
            if (dictionary.TryGetValue("en_US", out var value) || dictionary.TryGetValue("en_EN", out value))
            {
                
                return new KeyValuePair<string, string>("en_US", value);
            }
            else
            {
                return base.Handle(dictionary);
            }
        }
    }
}
