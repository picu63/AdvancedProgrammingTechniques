using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    class EnglishLanguageHandler : LanguageHandler
    {
        public override object Handle(Dictionary<string,string> request)
        {
            if (request.TryGetValue("en_EN", out var value))
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
