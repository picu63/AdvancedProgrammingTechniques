using System;
using System.Collections.Generic;
using LanguageProvider;

namespace LanguageTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var givenLanguageHandler = new GivenLanguageHandler();
            var englishLanguageHandler = new EnglishLanguageHandler();
            var polishLanguageHandler = new PolishLanguageHandler();
            var anyLanguageHandler = new AnyLanguageHandler();
            givenLanguageHandler
                .SetNext(englishLanguageHandler)
                .SetNext(polishLanguageHandler)
                .SetNext(anyLanguageHandler);

            var dictionary = new Dictionary<string, string>()
                {{"ens_US", "Napis po angielsku"}, {"cn_CN", "Napis po chińsku"}};
            const string searchKey = "cnd_CN";
            Console.WriteLine(dictionary);
            Console.WriteLine($"Poszukiwany klucz: {searchKey}");
            var result = givenLanguageHandler.Handle(dictionary, searchKey);
            Console.WriteLine($"Key: {result.Key}");
            Console.WriteLine($"Value: {result.Value}");
            Console.ReadKey();
        }
    }
}
