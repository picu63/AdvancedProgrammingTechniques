using System;
using System.Collections.Generic;
using System.Globalization;

namespace LanguageProvider
{
    class PolishLanguageHandler : LanguageHandler
    {
        public override object Handle(Dictionary<string, string> request)
        {
            if (request.TryGetValue("pl_PL", out var value))
            {
                return value;
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}