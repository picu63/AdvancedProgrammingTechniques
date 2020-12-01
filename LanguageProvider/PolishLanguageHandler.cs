using System;
using System.Collections.Generic;
using System.Globalization;

namespace LanguageProvider
{
    public class PolishLanguageHandler : AbstractLanguageHandler
    {
        public override KeyValueNullable<string, string> Handle(Dictionary<string, string> dictionary, string key)
        {
            return dictionary.TryGetValue("pl_PL", out var value) ? new KeyValueNullable<string, string>("pl_PL", value) : base.Handle(dictionary, key);
        }
    }
}