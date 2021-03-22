using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using OsuRTDataProvider.Listen;

namespace osuTools.PerformanceCalculator.Catch
{
    public class ValueObserver<T>
    {
        private T _val;
        private T _oldVal;
        
        public bool BreakWhenChange { get; set; }
        public ValueObserver(T val = default,bool breakWhenAssign = false)
        {
            
            _val = val;
            if (breakWhenAssign)
                Debugger.Break();
        }
        public T Value
        {
            get => _val;
            set
            {
                if (_val == null)
                {
                    
                    OnChanged(_val, value);
                    _val = value;
                    if (BreakWhenChange)
                        Debugger.Break();
                }
                else if (!_val.Equals(value))
                {
                    
                    _oldVal = _val;
                    OnChanged(_oldVal, value);
                    _val = value;
                    if (BreakWhenChange)
                        Debugger.Break();
                }
                
            }
        }

        public T OldValue
        {
            get => _oldVal;
        }

        public delegate void Changed(T oldVal, T val);

        public event Changed OnChanged = (oldVal, val) => { };

        public static implicit operator T(ValueObserver<T> observer)
        {
            return observer.Value;
        }
        public static implicit operator ValueObserver<T>(T val)
        {
            return new ValueObserver<T>(val);
        }

        public static ValueObserver<T> FromValue(T val, bool breakWhenAssign = false)
        {
            var c = new ValueObserver<T>(val);
            if(breakWhenAssign)
                Debugger.Break();
            return c;

        }
    }
}
