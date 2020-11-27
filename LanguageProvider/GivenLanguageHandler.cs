using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    class GivenLanguageHandler : LanguageHandler
    {
        private readonly string _key;

        public GivenLanguageHandler(string key)
        {
            _key = key;
        }
        public override object Handle(Dictionary<string, string> request)
        {
            if (request.TryGetValue(this._key, out var value))
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
