using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageProvider
{
    public class KeyValueNullable<TKey,TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public KeyValueNullable(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
