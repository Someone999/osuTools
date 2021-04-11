namespace osuTools.Skins.SkinObjects.Generic.PlayField.Countdown
{
    /// <summary>
    ///     倒计时
    /// </summary>
    public class CountdownImageCollection
    {
        /// <summary>
        ///     准备
        /// </summary>
        public ReadyCountdown Ready { get; internal set; } = new ReadyCountdown();

        /// <summary>
        ///     3
        /// </summary>
        public ReadyCountdown Three { get; internal set; } = new ReadyCountdown();

        /// <summary>
        ///     2
        /// </summary>
        public ReadyCountdown Two { get; internal set; } = new ReadyCountdown();

        /// <summary>
        ///     1
        /// </summary>
        public ReadyCountdown One { get; internal set; } = new ReadyCountdown();

        /// <summary>
        ///     开始
        /// </summary>
        public ReadyCountdown Go { get; internal set; } = new ReadyCountdown();
    }
}