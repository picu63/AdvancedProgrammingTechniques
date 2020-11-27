using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(object request);
    }
}
