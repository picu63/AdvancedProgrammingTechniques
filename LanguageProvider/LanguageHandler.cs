using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    abstract class LanguageHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;

            // Returning a handler from here will let us link handlers in a
            // convenient way like this:
            // monkey.SetNext(squirrel).SetNext(dog);
            return handler;
        }

        public virtual object Handle(Dictionary<string, string> request)
        {
            return _nextHandler?.Handle(request);
        }
    }
}
