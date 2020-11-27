using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanguageProvider
{
    public class AnyLanguageHandler : LanguageHandler
    {
        public override object Handle(Dictionary<string, string> dictionary)
        {
            if (dictionary.Any())
            {
                return dictionary.First().Value;
            }
            else
            {
                return null;
            }
        }
    }
}
