using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    public class GivenLanguageHandler : LanguageHandler
    {
        private readonly string _key;

        public GivenLanguageHandler(string key)
        {
            _key = key;
        }
        public override object Handle(Dictionary<string, string> dictionary)
        {
            if (dictionary.TryGetValue(this._key, out var value))
            {
                return new KeyValuePair<string, string>(_key, value);
            }
            else
            {
                return base.Handle(dictionary);
            }
        }
    }
}
