using System.Collections;
using System.Collections.Generic;

namespace osuTools.Collections
{
    class DictionaryEnumerator<TKey,TValue>:IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private List<KeyValuePair<TKey, TValue>> _innerDict;
        private int _len = 0, _pos = -1;
        public KeyValuePair<TKey, TValue> Current => _innerDict[_pos];
        object IEnumerator.Current => Current;
        public void Dispose()
        {
        }
        public bool MoveNext() => ++_pos <= _len;
        public void Reset() => _pos = -1;

        public DictionaryEnumerator(Dictionary<TKey, TValue> dict)
        {
            _innerDict = dict._pairs;
            _len = _innerDict.Count;
        }
    }
}