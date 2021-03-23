using System.Collections;
using System.Collections.Generic;

namespace osuTools.PerformanceCalculator.Catch
{
    public class ObserveListEnumerator<T>:IEnumerator<T>
    {
        private ObservableList<T> arr;
        private int _len, _pos = -1;

        public ObserveListEnumerator(ObservableList<T> list)
        {
            arr = list;
            _len = list.Count;
        }

        public T Current
        {
            get => arr[_pos];
        }

        object IEnumerator.Current => Current;
        public void Dispose()
        {
        }
        public bool MoveNext() => ++_pos < _len;
        public void Reset() => _pos = -1;
    }
}