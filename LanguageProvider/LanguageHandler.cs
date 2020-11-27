using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    public abstract class LanguageHandler : IHandler
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

        public virtual object Handle(Dictionary<string, string> dictionary)
        {
            return _nextHandler?.Handle(dictionary);
        }
    }
}
