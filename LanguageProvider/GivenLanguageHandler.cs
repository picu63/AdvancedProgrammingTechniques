using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    public class GivenLanguageHandler : AbstractLanguageHandler
    {
        public override KeyValueNullable<string, string> Handle(Dictionary<string, string> dictionary, string key)
        {
            return dictionary.TryGetValue(key, out var value) ? new KeyValueNullable<string, string>(key, value) : base.Handle(dictionary, key);
        }
    }
}
