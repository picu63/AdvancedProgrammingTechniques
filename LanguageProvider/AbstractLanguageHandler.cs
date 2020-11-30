using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    public abstract class AbstractLanguageHandler : ILanguageHandler
    {
        private ILanguageHandler _nextLanguageHandler;

        public ILanguageHandler SetNext(ILanguageHandler languageHandler)
        {
            _nextLanguageHandler = languageHandler;
            return languageHandler;
        }

        public virtual KeyValueNullable<string, string> Handle(Dictionary<string, string> dictionary, string key)
        {
            if (_nextLanguageHandler != null)
            {
                return _nextLanguageHandler.Handle(dictionary, key);
            }
            else
            {
                return null;
            }
        }
    }
}

