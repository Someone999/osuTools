using System.Collections;
using System.Collections.Generic;

namespace osuTools.Collections
{

    class ObservableListEnumerator<T> : IEnumerator<T>
    {
        private readonly ObservableList<T> _innerList;
        private readonly int _len;
        private int _pos = -1;

        internal ObservableListEnumerator(ObservableList<T> observableList)
        {
            _innerList = observableList;
            _len = _innerList.Count;
        }
        ///<inheritdoc/>
        public bool MoveNext() => ++_pos <= _len - 1;
        ///<inheritdoc/>
        public T Current => _innerList[_pos];
        object IEnumerator.Current => Current;
        ///<inheritdoc/>
        public void Dispose()
        {
        }
        ///<inheritdoc/>
        public void Reset() => _pos = -1;
    }
}