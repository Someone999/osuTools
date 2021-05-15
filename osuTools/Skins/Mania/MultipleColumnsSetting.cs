using System;

namespace osuTools.Skins.Mania
{
    /// <summary>
    ///     Mania皮肤各个列数的设置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MultipleColumnsSetting<T> where T : new()
    {
        /// <summary>
        /// 第1列的设置
        /// </summary>
        public T Column1 { get; internal set; } = new T();
        /// <summary>
        /// 第2列的设置
        /// </summary>
        public T Column2 { get; internal set; } = new T();
        /// <summary>
        /// 第3列的设置
        /// </summary>
        public T Column3 { get; internal set; } = new T();
        /// <summary>
        /// 第4列的设置
        /// </summary>
        public T Column4 { get; internal set; } = new T();
        /// <summary>
        /// 第5列的设置
        /// </summary>
        public T Column5 { get; internal set; } = new T();
        /// <summary>
        /// 第6列的设置
        /// </summary>
        public T Column6 { get; internal set; } = new T();
        /// <summary>
        /// 第7列的设置
        /// </summary>
        public T Column7 { get; internal set; } = new T();
        /// <summary>
        /// 第8列的设置
        /// </summary>
        public T Column8 { get; internal set; } = new T();
        /// <summary>
        /// 第9列的设置
        /// </summary>
        public T Column9 { get; internal set; } = new T();
        /// <summary>
        /// 第10列的设置
        /// </summary>
        public T Column10 { get; internal set; } = new T();
        /// <summary>
        /// 第11列的设置
        /// </summary>
        public T Column11 { get; internal set; } = new T();
        /// <summary>
        /// 第12列的设置
        /// </summary>
        public T Column12 { get; internal set; } = new T();
        /// <summary>
        /// 第13列的设置
        /// </summary>
        public T Column13 { get; internal set; } = new T();
        /// <summary>
        /// 第14列的设置
        /// </summary>
        public T Column14 { get; internal set; } = new T();
        /// <summary>
        /// 第15列的设置
        /// </summary>
        public T Column15 { get; internal set; } = new T();
        /// <summary>
        /// 第16列的设置
        /// </summary>
        public T Column16 { get; internal set; } = new T();
        /// <summary>
        /// 第17列的设置
        /// </summary>
        public T Column17 { get; internal set; } = new T();
        /// <summary>
        /// 第18列的设置
        /// </summary>
        public T Column18 { get; internal set; } = new T();

        /// <summary>
        ///     将所有的列数的值设定为指定值
        /// </summary>
        /// <param name="val"></param>
        public void SetForAllColumns(T val)
        {
            Column1 = val;
            Column2 = val;
            Column3 = val;
            Column4 = val;
            Column5 = val;
            Column6 = val;
            Column7 = val;
            Column8 = val;
            Column9 = val;
            Column10 = val;
            Column11 = val;
            Column12 = val;
            Column13 = val;
            Column14 = val;
            Column15 = val;
            Column16 = val;
            Column17 = val;
            Column18 = val;
        }

        /// <summary>
        ///     将指定列数的值设置为指定值
        /// </summary>
        /// <param name="column"></param>
        /// <param name="val"></param>
        public void SetForColumn(int column, T val)
        {
            switch (column)
            {
                case 0:
                    Column1 = val;
                    break;
                case 1:
                    Column2 = val;
                    break;
                case 2:
                    Column3 = val;
                    break;
                case 3:
                    Column4 = val;
                    break;
                case 4:
                    Column5 = val;
                    break;
                case 5:
                    Column6 = val;
                    break;
                case 6:
                    Column7 = val;
                    break;
                case 7:
                    Column8 = val;
                    break;
                case 8:
                    Column9 = val;
                    break;
                case 9:
                    Column10 = val;
                    break;
                case 10:
                    Column11 = val;
                    break;
                case 11:
                    Column12 = val;
                    break;
                case 12:
                    Column13 = val;
                    break;
                case 13:
                    Column14 = val;
                    break;
                case 14:
                    Column15 = val;
                    break;
                case 15:
                    Column16 = val;
                    break;
                case 16:
                    Column17 = val;
                    break;
                case 17:
                    Column18 = val;
                    break;
                default: throw new ArgumentException("索引必须是一个0-17的整数。");
            }
        }
    }
}