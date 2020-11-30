using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    public class EnglishLanguageHandler : AbstractLanguageHandler
    {
        public override KeyValueNullable<string, string> Handle(Dictionary<string, string> dictionary, string key)
        {
            if (dictionary.TryGetValue("en_US", out var value))
            {
                
                return new KeyValueNullable<string, string>("en_US", value);
            }

            return base.Handle(dictionary, key);
        }
    }
}
