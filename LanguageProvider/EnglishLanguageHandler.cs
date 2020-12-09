using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    public class EnglishLanguageHandler : AbstractLanguageHandler
    {
        public override KeyValueNullable<string, string> Handle(Dictionary<string, string> dictionary, string key)
        {
            return dictionary.TryGetValue("en_US", out var value) ? new KeyValueNullable<string, string>("en_US", value) : base.Handle(dictionary, key);
        }
    }
}
