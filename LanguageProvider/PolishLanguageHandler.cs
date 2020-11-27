using System;
using System.Collections.Generic;
using System.Globalization;

namespace LanguageProvider
{
    public class PolishLanguageHandler : LanguageHandler
    {
        public override object Handle(Dictionary<string, string> dictionary)
        {
            if (dictionary.TryGetValue("pl_PL", out var value))
            {
                return value;
            }

            return base.Handle(dictionary);
        }
    }
}