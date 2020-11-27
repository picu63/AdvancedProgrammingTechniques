using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(Dictionary<string, string> request);
    }
}
