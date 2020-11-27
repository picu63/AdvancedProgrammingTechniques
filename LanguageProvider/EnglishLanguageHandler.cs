using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    public class EnglishLanguageHandler : LanguageHandler
    {
        public override object Handle(Dictionary<string,string> dictionary)
        {
            if (dictionary.TryGetValue("en_EN", out var value))
            {
                return value;
            }
            else
            {
                return base.Handle(dictionary);
            }
        }
    }
}
