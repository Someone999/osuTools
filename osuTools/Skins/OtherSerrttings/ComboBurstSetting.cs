namespace osuTools.Skins.OtherSerrttings
{
    /// <summary>
    ///     达到指定连击时的皮肤设置
    /// </summary>
    public class ComboBurstSetting
    {
        /// <summary>
        ///     随机显示ComboBurst
        /// </summary>
        public bool ComboBurstRandom { get; internal set; } = false;

        /// <summary>
        ///     使用自定义的ComboBurst的音频
        /// </summary>
        public string CustomComboBurstSound { get; internal set; } = "";
    }
}