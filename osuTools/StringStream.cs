using System;
using System.IO;
using System.Text;

namespace osuTools
{
    /// <summary>
    /// 一个存储字符串的流
    /// </summary>
    public sealed class StringStream : Stream
    {
        private byte[] _strBytes;
        private long _capacity;
        private long _currentPos;
        private string _innerString;
        /// <summary>
        /// 流使用的编码器
        /// </summary>
        public Encoding StringEncoding { get; set; }
        /// <summary>
        /// 获取流中的字符
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            Flush();
            return _innerString;
        }
        /// <summary>
        /// 将流中的已有的字符写入字符串
        /// </summary>
        public override void Flush() => _innerString = StringEncoding.GetString(_strBytes);

        /// <summary>
        /// 获取流中存储的字符
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes() => (byte[])_strBytes.Clone();

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
        /// <summary>
        /// 使用指定的编码器初始化StringStream，编码默认为UTF8
        /// </summary>
        /// <param name="encoding">要使用的编码，默认为UTF8</param>
        public StringStream(Encoding encoding = null)
        {
            StringEncoding = encoding ?? Encoding.UTF8;
            _capacity = 4;
            _strBytes = new byte[4];
        }
        /// <summary>
        /// 使用指定的初始容量和编码器初始化StringStream，编码默认为UTF8
        /// </summary>
        /// <param name="capacity">初始容量</param>
        /// <param name="encoding">编码器，默认为UTF8</param>
        public StringStream(int capacity, Encoding encoding = null)
        {
            StringEncoding = encoding ?? Encoding.UTF8;
            _capacity = capacity;
            _strBytes = new byte[capacity];
        }
        /// <summary>
        /// 使用一个对象ToString()的返回结果填充StringStream，编码默认为UTF8
        /// </summary>
        /// <param name="convertable">要转换成字符串的对象</param>
        /// <param name="encoding">编码器，默认为UTF8</param>
        public StringStream(object convertable, Encoding encoding = null)
        {
            StringEncoding = encoding ?? Encoding.UTF8;
            var bts = StringEncoding.GetBytes(convertable.ToString());
            _strBytes = new byte[0];
            Write(bts, 0, bts.Length);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override bool CanSeek => false;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override bool CanRead => true;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override bool CanWrite => true;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override int Read(byte[] buffer, int offset, int count)
        {
            Array.Copy(_strBytes, offset, buffer, 0, Math.Min(count, _strBytes.Length));
            return Math.Min(count, _strBytes.Length);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException("为了避免字符串损坏，不允许对字符串流进行Seek");

        /// <summary>
        /// 将字符串写入流
        /// </summary>
        public void Write(string str)
        {
            byte[] sBytes = StringEncoding.GetBytes(str);
            Write(sBytes, 0, sBytes.Length);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Write(byte[] buffer, int offset, int count)
        {
            Extend(count);
            Array.Copy(buffer, offset, _strBytes, _currentPos, count);
            _currentPos += count;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override long Position
        {
            get => _currentPos;
            set => throw new NotSupportedException("为了避免字符串损坏，不允许对字符串流进行指针定位");
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override long Length => _strBytes.Length;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void SetLength(long value) => Extend(value, true);
    }
}