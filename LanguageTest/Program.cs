using System;
using System.Collections.Generic;
using LanguageProvider;

namespace LanguageTest
{
    class Program
    {
        static void Main(string[] args)
        {
            EnglishLanguageHandler englishLanguageHandler = new EnglishLanguageHandler();
            PolishLanguageHandler polishLanguageHandler = new PolishLanguageHandler();
            AnyLanguageHandler anyLanguageHandler = new AnyLanguageHandler();
            var givenLanguageHandler = new GivenLanguageHandler("klucz")
                .SetNext(englishLanguageHandler)
                .SetNext(polishLanguageHandler)
                .SetNext(anyLanguageHandler);

            givenLanguageHandler.Handle(new Dictionary<string, string>());
        }
    }
}
