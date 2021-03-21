using osuTools.Skins.Colors;
using osuTools.Skins.Settings.Mania.MultipleColumnsSettings;
using osuTools.Skins.SkinObjects.Mania;

namespace osuTools.Skins.Settings.Mania
{
    public class ManiaSkinSetting
    {
        public double ColumnStart { get; internal set; } = 136;
        public double ColumnRight { get; internal set; } = 19;
        public string ColumnSpacing { get; internal set; } = "0";
        public string ColumnWidth { get; internal set; } = "30";
        public string ColumnLineWidth { get; internal set; } = "2";
        public double BarlineHeight { get; internal set; } = 1.2;
        public string LightingNWidth { get; internal set; } = "";
        public string LightingLWidth { get; internal set; } = "";
        public double? WidthForNoteHeightScale { get; internal set; } = null;
        public int HitPosition { get; internal set; } = 402;
        public int LightPosition { get; internal set; } = 413;
        public int? ScorePosition { get; internal set; } = null;
        public int? ComboPosition { get; internal set; } = null;
        public bool JudgementLine { get; internal set; } = false;
        public SpecialStyles SpecialStyle { get; internal set; } = SpecialStyles.None;
        public ComboBurstStyles ComboBurstStyle { get; internal set; } = ComboBurstStyles.Right;
        public bool? SplitStages { get; internal set; } = null;
        public double StageSeparation { get; internal set; } = 40;
        public bool SeparateScore { get; internal set; } = true;
        public bool KeysUnderNotes { get; internal set; } = false;
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