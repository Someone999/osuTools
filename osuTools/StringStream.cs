using System;
using System.IO;
using System.Text;

namespace CSarp_Console_Test
{
    sealed class StringStream : Stream
    {
        private byte[] _strBytes;
        private long _capacity;
        private long _currentPos;
        private string _innerString;
        public Encoding StringEncoding { get; set; }

        public string GetString()
        {
            Flush();
            return _innerString;
        }
        public override void Flush()
        {
            _innerString = StringEncoding.GetString(_strBytes);
        }

        public byte[] GetBytes()
        {
            return (byte[])_strBytes.Clone();
        }
        void Extend(long requiredSize, bool forced = false)
        {
            if (forced)
            {
                long nCapacity = requiredSize;
                if (requiredSize == _capacity)
                    return;
                if (requiredSize < _capacity)
                    throw new ArgumentException("重新设置的长度小于初始长度");
                byte[] nArr = new byte[nCapacity];
                Array.Copy(_strBytes, nArr, _strBytes.Length);
                _strBytes = nArr;
                _capacity = nCapacity;
            }
            else if (_capacity < _capacity + requiredSize)
            {
                long realSize = _capacity + requiredSize;
                long nCapacity = _capacity == 0 ? Math.Max(realSize + 1, 4) : Math.Max(realSize + 1, _capacity * 2);
                byte[] nArr = new byte[nCapacity];
                Array.Copy(_strBytes, nArr, _strBytes.Length);
                _strBytes = nArr;
                _capacity = nCapacity;
            }


        }
        public StringStream(Encoding encoding = null)
        {
            StringEncoding = encoding ?? Encoding.UTF8;
            _capacity = 4;
            _strBytes = new byte[4];
        }
        public StringStream(int capacity, Encoding encoding = null)
        {
            StringEncoding = encoding ?? Encoding.UTF8;
            _capacity = capacity;
            _strBytes = new byte[capacity];
        }
        public StringStream(object convertable, Encoding encoding = null)
        {
            StringEncoding = encoding ?? Encoding.UTF8;
            var bts = StringEncoding.GetBytes(convertable.ToString());
            _strBytes = new byte[0];
            Write(bts, 0, bts.Length);
        }
        public override bool CanSeek => false;
        public override bool CanRead => true;
        public override bool CanWrite => true;
        public override int Read(byte[] buffer, int offset, int count)
        {
            Array.Copy(_strBytes, offset, buffer, 0, Math.Min(count, _strBytes.Length));
            return count;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("为了避免字符串损坏，不允许对字符串流进行Seek");
        }

        public void Write(string str)
        {
            byte[] sBytes = StringEncoding.GetBytes(str);
            Write(sBytes, 0, sBytes.Length);
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            Extend(count);
            Array.Copy(buffer, offset, _strBytes, _currentPos, count);
            _currentPos += count;
        }

        public override long Position
        {
            get => _currentPos;
            set => throw new NotSupportedException("为了避免字符串损坏，不允许对字符串流进行指针定位");
        }

        public override long Length => _strBytes.Length;
        public override void SetLength(long value)
        {
            Extend(value, true);
        }
    }
}