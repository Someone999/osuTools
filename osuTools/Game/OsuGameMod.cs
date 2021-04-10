namespace osuTools
{
    /// <summary>
    ///     游戏中的Mod
    /// </summary>
    public enum OsuGameMod
    {
        /// <summary>
        ///     无Mod
        /// </summary>
        None = 0,

        /// <summary>
        ///     不死
        /// </summary>
        NoFail = 1 << 0,

        /// <summary>
        ///     降低谱面难度，并有3次满状态复活机会
        /// </summary>
        Easy = 1 << 1,

        /// <summary>
        ///     触屏设备
        /// </summary>
        TouchDevice = 1 << 2, //TouchDevice

        /// <summary>
        ///     渐隐
        /// </summary>
        Hidden = 1 << 3, //HD

        /// <summary>
        ///     增加谱面难度，并将osu!模式note上下翻转
        /// </summary>
        HardRock = 1 << 4, //HR

        /// <summary>
        ///     掉一个就死
        /// </summary>
        SuddenDeath = 1 << 5, //SD

        /// <summary>
        ///     将谱面加速至1.25倍速
        /// </summary>
        DoubleTime = 1 << 6, //DT

        /// <summary>
        ///     自动点击
        /// </summary>
        Relax = 1 << 7, //RX

        /// <summary>
        ///     将谱面减速至0.75倍速
        /// </summary>
        HalfTime = 1 << 8, //HT

        /// <summary>
        ///     将谱面加速1.25倍速并加重节奏
        /// </summary>
        NightCore = 1 << 9, //NC

        /// <summary>
        ///     只显示光标附近区域
        /// </summary>
        Flashlight = 1 << 10, //FL

        /// <summary>
        ///     自动游玩
        /// </summary>
        AutoPlay = 1 << 11, //Auto

        /// <summary>
        ///     转盘自动转动
        /// </summary>
        SpunOut = 1 << 12, //SP

        /// <summary>
        ///     光标自动定位
        /// </summary>
        AutoPilot = 1 << 13, //AP

        /// <summary>
        ///     要么SS，要么重来
        /// </summary>
        Perfect = 1 << 14,

        /// <summary>
        ///     将键位转化为4个
        /// </summary>
        Key4 = 1 << 15,

        /// <summary>
        ///     将键位转化为5个
        /// </summary>
        Key5 = 1 << 16,

        /// <summary>
        ///     将键位转化为6个
        /// </summary>
        Key6 = 1 << 17,

        /// <summary>
        ///     将键位转化为7个
        /// </summary>
        Key7 = 1 << 18, //7K

        /// <summary>
        ///     将键位转化为8个
        /// </summary>
        Key8 = 1 << 19,

        /// <summary>
        ///     上隐
        /// </summary>
        FadeIn = 1 << 20, //FD

        /// <summary>
        ///     随机排布Note
        /// </summary>
        Random = 1 << 21, //RD

        /// <summary>
        ///     隐藏谱面图层
        /// </summary>
        Cinema = 1 << 22, //CN

        /// <summary>
        ///     暂无说明
        /// </summary>
        Target = 1 << 23, //Target

        /// <summary>
        ///     将键位转化为9个
        /// </summary>
        Key9 = 1 << 24, //9K

        /// <summary>
        ///     将一个游玩区域转化为2个，以便双人游玩
        /// </summary>
        KeyCoop = 1 << 25, //Co-op

        /// <summary>
        ///     将键位转化为1个
        /// </summary>
        Key1 = 1 << 26, //1K

        /// <summary>
        ///     将键位转化为3个
        /// </summary>
        Key3 = 1 << 27, //2K

        /// <summary>
        ///     将键位转化为2个
        /// </summary>
        Key2 = 1 << 28, //3K

        /// <summary>
        ///     新版计分方式
        /// </summary>
        ScoreV2 = 1 << 29, //V2

        /// <summary>
        ///     镜像
        /// </summary>
        Mirror = 1 << 30, //MR

        /// <summary>
        ///     转换Osu模式到Mania模式转谱的键位的数量的Mod
        /// </summary>
        KeyMod = Key1 | Key2 | Key3 | Key4 | Key5 | Key6 | Key7 | Key8 | Key9,

        /// <summary>
        ///     未定义
        /// </summary>
        Unknown = -1,

        /// <summary>
        ///     多人游戏中可自由选择的Mod
        /// </summary>
        FreeModAllowed = NoFail | Easy | Hidden | HardRock | SuddenDeath | Flashlight | FadeIn | Relax | AutoPilot |
                         SpunOut | KeyMod,

        /// <summary>
        ///     提升分数的Mod
        /// </summary>
        ScoreIncreaseMods = Hidden | HardRock | DoubleTime | Flashlight | FadeIn
    }
}