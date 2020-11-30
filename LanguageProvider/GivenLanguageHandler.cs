using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    public class GivenLanguageHandler : AbstractLanguageHandler
    {

        public override KeyValueNullable<string, string> Handle(Dictionary<string, string> dictionary, string key)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                return new KeyValueNullable<string, string>(key, value);
            }

            return base.Handle(dictionary, key);
        }
    }
}
