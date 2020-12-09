using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageProvider
{
    public class AnyLanguageHandler : AbstractLanguageHandler
    {
        public override KeyValueNullable<string, string> Handle(Dictionary<string, string> dictionary, string key)
        {
            if (dictionary.Any())
            {
                var firstPair = dictionary.FirstOrDefault();
                var value = firstPair.Value;
                var firstKey = firstPair.Key;
                return new KeyValueNullable<string, string>(firstKey,value);
            }

            return base.Handle(dictionary, key);
        }
    }
}
