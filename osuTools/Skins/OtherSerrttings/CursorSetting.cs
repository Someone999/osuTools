namespace osuTools.Skins.Settings.Cursor
{
    public class CursorSetting
    {
        /// <summary>
        ///     光标居中
        /// </summary>
        public bool CursorCenter { get; internal set; } = true;

        /// <summary>
        ///     扩大光标
        /// </summary>
        public bool CursorExpand { get; internal set; } = true;

        /// <summary>
        ///     光标旋转
        /// </summary>
        public bool CursorRotate { get; internal set; } = true;

        /// <summary>
        ///     光标有拖尾
        /// </summary>
        public bool CursorTrailRotate { get; internal set; } = true;
    }
}