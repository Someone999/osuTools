using System.Collections;
using System.Collections.Generic;

namespace osuTools.PerformanceCalculator.Catch
{
    public class ObservableListEnumerator<T> : IEnumerator<T>
    {
        private ObservableList<T> _innerList;
        private int _len, _pos;

        public ObservableListEnumerator(ObservableList<T> observableList)
        {
            _innerList = observableList;
        }

        public bool MoveNext() => ++_pos <= _len - 1;
        public T Current => _innerList[_pos];
        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
        public void Reset() => _pos = -1;
    }
}