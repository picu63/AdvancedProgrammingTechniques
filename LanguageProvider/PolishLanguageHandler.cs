using System;
using System.Collections.Generic;
using System.Globalization;

namespace LanguageProvider
{
    public class PolishLanguageHandler : AbstractLanguageHandler
    {
        public override KeyValueNullable<string, string> Handle(Dictionary<string, string> dictionary, string key)
        {
            if (dictionary.TryGetValue("pl_PL", out var value))
            {
                return new KeyValueNullable<string, string>("pl_PL", value);
            }

            return base.Handle(dictionary, key);
        }
    }
}