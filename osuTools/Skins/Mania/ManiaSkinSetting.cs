using osuTools.Skins.Colors;
using osuTools.Skins.Settings.Mania.MultipleColumnsSettings;
using osuTools.Skins.SkinObjects.Mania;

namespace osuTools.Skins.Settings.Mania
{
    public class ManiaSkinSetting
    {
        /// <summary>
        /// 列开始的位置
        /// </summary>
        public double ColumnStart { get; internal set; } = 136;
        public double ColumnRight { get; internal set; } = 19;
        /// <summary>
        /// 列间隙，有多列时用半角逗号隔开
        /// </summary>
        public string ColumnSpacing { get; internal set; } = "0";
        /// <summary>
        /// 列宽，有多列时用半角逗号隔开
        /// </summary>
        public string ColumnWidth { get; internal set; } = "30";
        /// <summary>
        /// 列间线的宽度
        /// </summary>
        public string ColumnLineWidth { get; internal set; } = "2";
        /// <summary>
        /// 小节线的宽度
        /// </summary>
        public double BarlineHeight { get; internal set; } = 1.2;
        public string LightingNWidth { get; internal set; } = "";
        public string LightingLWidth { get; internal set; } = "";
        public double? WidthForNoteHeightScale { get; internal set; } = null;
        /// <summary>
        /// 判定线位置
        /// </summary>
        public int HitPosition { get; internal set; } = 402;
        /// <summary>
        /// 闪光的位置
        /// </summary>
        public int LightPosition { get; internal set; } = 413;
        /// <summary>
        /// 判定显示的位置
        /// </summary>
        public int? ScorePosition { get; internal set; } = null;
        /// <summary>
        /// 连击显示的位置
        /// </summary>
        public int? ComboPosition { get; internal set; } = null;
        /// <summary>
        /// 判定线的宽度
        /// </summary>
        public int JudgementLine { get; internal set; } = 0;
        /// <summary>
        /// 特殊样式
        /// </summary>
        public SpecialStyles SpecialStyle { get; internal set; } = SpecialStyles.None;
        /// <summary>
        /// 连击指示器样式
        /// </summary>
        public ComboBurstStyles ComboBurstStyle { get; internal set; } = ComboBurstStyles.Right;
        /// <summary>
        /// 将界面分割
        /// </summary>
        public bool? SplitStages { get; internal set; } = null;
        public double StageSeparation { get; internal set; } = 40;
        public bool SeparateScore { get; internal set; } = true;
        /// <summary>
        /// Keys会被Note覆盖
        /// </summary>
        public bool KeysUnderNotes { get; internal set; } = false;
        /// <summary>
        /// Note是否从下至上
        /// </summary>
        public bool UpsideDown { get; internal set; } = false;

        public MultipleColumnsSetting<bool> KeyFlipWhenUpsideDown { get; internal set; } =
            new MultipleColumnsSetting<bool>();

        public MultipleColumnsSetting<bool> KeyFlipWhenUpsideDownD { get; internal set; } =
            new MultipleColumnsSetting<bool>();

        public MultipleColumnsSetting<bool> NoteFlipWhenUpsideDown { get; internal set; } =
            new MultipleColumnsSetting<bool>();

        public MultipleColumnsSetting<bool> NoteFlipWhenUpsideDownL { get; internal set; } =
            new MultipleColumnsSetting<bool>();

        public MultipleColumnsSetting<bool> NoteFlipWhenUpsideDownH { get; internal set; } =
            new MultipleColumnsSetting<bool>();

        public MultipleColumnsSetting<bool> NoteFlipWhenUpsideDownT { get; internal set; } =
            new MultipleColumnsSetting<bool>();

        public MultipleColumnsSetting<int> NoteBodyStyle { get; internal set; } = new MultipleColumnsSetting<int>();
        public MultipleColumnsSetting<RGBAColor> Color { get; internal set; } = new MultipleColumnsSetting<RGBAColor>();

        public MultipleColumnsSetting<RGBColor> ColorLight { get; internal set; } =
            new MultipleColumnsSetting<RGBColor>();

        public RGBAColor ColorColumnLine { get; internal set; } = new RGBAColor(255, 255, 255);
        public RGBAColor ColorBarline { get; internal set; } = new RGBAColor(255, 255, 255);
        public RGBColor ColorJudgementLine { get; internal set; } = new RGBColor(255, 255, 255);
        public RGBColor ColorKeyWarning { get; internal set; } = new RGBColor(0, 0, 0);
        public RGBAColor ColorHold { get; internal set; } = new RGBAColor(255, 191, 51);
        public RGBColor ColorBreak { get; internal set; } = new RGBColor(255, 0, 0);

        public ManiaSkinImageCollection SkinImages { get; internal set; } = new ManiaSkinImageCollection();
    }
}