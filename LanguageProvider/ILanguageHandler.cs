using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    public interface ILanguageHandler
    {
        ILanguageHandler SetNext(ILanguageHandler languageHandler);

        KeyValueNullable<string, string> Handle(Dictionary<string, string> dictionary, string key);
    }
}
