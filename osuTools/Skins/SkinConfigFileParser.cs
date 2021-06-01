using System;
using System.Linq;
using osuTools.Skins.Catch;
using osuTools.Skins.Color;
using osuTools.Skins.Fonts;
using osuTools.Skins.Mania;

namespace osuTools.Skins
{
    public partial class Skin
    {
        private void GetInfo()
        {
            var currentKey = 0;
            var block = "";
            FontSettings = new FontSetting();
            ColorSettings = new ColorSetting();
            CatchSettings = new CatchSkinSetting();
            ManiaSettings = new MultipleKeysSettings();
            foreach (var line in _data)
            {
                var val = line.Split(':');
                if (line.Trim().StartsWith("[") && line.Trim().EndsWith("]"))
                    block = line.Trim().Trim('[', ']');

                if (val.Length > 1)
                {
                    var v = val[1].Trim();
                    var k = val[0].Trim();
                    if (line.Trim().StartsWith("Name")) Name = v;
                    if (line.Trim().StartsWith("Author")) Author = v;
                    if (line.Trim().StartsWith("Version")) Version = v;
                    if (line.Trim().StartsWith("AnimationFramerate")) AnimationFrameRate = v.ToUInt32();
                    if (line.Trim().StartsWith("AllowSliderBallTint")) SliderSettings.AllowSliderBallTint = v.ToBool();
                    if (line.Trim().StartsWith("ComboBurstRandom")) ComboBurstSettings.ComboBurstRandom = v.ToBool();
                    if (line.Trim().StartsWith("CursorCentre")) CursorSettings.CursorCenter = v.ToBool();
                    if (line.Trim().StartsWith("CursorExpand")) CursorSettings.CursorExpand = v.ToBool();
                    if (line.Trim().StartsWith("CursorRotate")) CursorSettings.CursorRotate = v.ToBool();
                    if (line.Trim().StartsWith("CursorTrailRotate")) CursorSettings.CursorTrailRotate = v.ToBool();
                    if (line.Trim().StartsWith("CustomComboBurstSounds")) ComboBurstSettings.CustomComboBurstSound = v;
                    if (line.Trim().StartsWith("HitCircleOverlayAboveNumber")) HitCircleOverlayAboveNumber = v.ToBool();
                    if (line.Trim().StartsWith("LayeredHitSounds")) LayeredHitSounds = v.ToBool();
                    if (line.Trim().StartsWith("LayeredHitSounds")) LayeredHitSounds = v.ToBool();
                    if (line.Trim().StartsWith("SliderBallFlip")) SliderSettings.SliderBallFlip = v.ToBool();
                    if (line.Trim().StartsWith("SliderBallFrames")) SliderSettings.SliderBallFrames = v.ToUInt32();
                    if (line.Trim().StartsWith("SliderStyle")) SliderSettings.SliderStyle = (SliderStyles) v.ToInt32();
                    if (line.Trim().StartsWith("SpinnerFadePlayfield"))
                        SpinnerSettings.SpinnerFadePlayfield = v.ToBool();
                    if (line.Trim().StartsWith("SpinnerFrequencyModulate"))
                        SpinnerSettings.SpinnerFrequencyModulate = v.ToBool();
                    if (line.Trim().StartsWith("SpinnerNoBlink")) SpinnerSettings.SpinnerNoBlink = v.ToBool();
                    if (line.Trim().StartsWith("HyperDash")) CatchSettings.HyperDash = v.ToRgbColor();
                    if (line.Trim().StartsWith("HyperDashFruit")) CatchSettings.HyperDashFruit = v.ToRgbColor();
                    if (line.Trim().StartsWith("HyperDashAfterImage"))
                        CatchSettings.HyperDashAfterImage = v.ToRgbColor();
                    if (line.Trim().StartsWith("Combo1"))
                        ColorSettings.ComboColors.setColor(ComboNumber.Last, v.ToRgbColor() as ComboColor);
                    if (line.Trim().StartsWith("Combo2"))
                        ColorSettings.ComboColors.setColor(ComboNumber.First, v.ToRgbColor() as ComboColor);
                    if (line.Trim().StartsWith("Combo3"))
                        ColorSettings.ComboColors.setColor(ComboNumber.Second, v.ToRgbColor() as ComboColor);
                    if (line.Trim().StartsWith("Combo4"))
                        ColorSettings.ComboColors.setColor(ComboNumber.Third, v.ToRgbColor() as ComboColor);
                    if (line.Trim().StartsWith("Combo5"))
                        ColorSettings.ComboColors.setColor(ComboNumber.Fourth, v.ToRgbColor() as ComboColor);
                    if (line.Trim().StartsWith("Combo6"))
                        ColorSettings.ComboColors.setColor(ComboNumber.Fifth, v.ToRgbColor() as ComboColor);
                    if (line.Trim().StartsWith("Combo7"))
                        ColorSettings.ComboColors.setColor(ComboNumber.Sixth, v.ToRgbColor() as ComboColor);
                    if (line.Trim().StartsWith("Combo8"))
                        ColorSettings.ComboColors.setColor(ComboNumber.Seventh, v.ToRgbColor() as ComboColor);
                    if (line.Trim().StartsWith("InputOverlayText"))
                        ColorSettings.InputOverlayText = v.ToRgbColor() as TextColor;
                    if (line.Trim().StartsWith("MenuGlow")) ColorSettings.MenuGlow = v.ToRgbColor() as OverlayColor;
                    if (line.Trim().StartsWith("SliderBall")) ColorSettings.SliderBall = v.ToRgbColor();
                    if (line.Trim().StartsWith("SliderBorder")) ColorSettings.SliderBorder = v.ToRgbColor();
                    if (line.Trim().StartsWith("SliderTrackOverride"))
                        ColorSettings.SliderTrackOverride = v.ToRgbColor();
                    if (line.Trim().StartsWith("SongSelectActiveText"))
                        ColorSettings.SongSelectActiveText = v.ToRgbColor() as TextColor;
                    if (line.Trim().StartsWith("SongSelectInactiveText"))
                        ColorSettings.SongSelectInactiveText = v.ToRgbColor() as TextColor;
                    if (line.Trim().StartsWith("SpinnerBackground"))
                        ColorSettings.SpinnerBackground = v.ToRgbColor() as OverlayColor;
                    if (line.Trim().StartsWith("StarBreakAdditive"))
                        ColorSettings.StarBreakAdditive = v.ToRgbColor() as OverlayColor;
                    if (line.Trim().StartsWith("HitCirclePrefix")) FontSettings.HitCirclePrefix = v;
                    if (line.Trim().StartsWith("HitCircleOverlap")) FontSettings.HitCircleOverlap = v.ToInt32();
                    if (line.Trim().StartsWith("ScorePrefix")) FontSettings.ScorePrefix = v;
                    if (line.Trim().StartsWith("ScoreOverlap")) FontSettings.ScoreOverlap = v.ToInt32();
                    if (line.Trim().StartsWith("ComboPrefix")) FontSettings.ComboPrefix = v;
                    if (line.Trim().StartsWith("ComboOverlap")) FontSettings.ComboOverlap = v.ToInt32();
                    if (block == "Mania")
                    {
                        if (line.Trim().StartsWith("Keys:")) currentKey = v.ToInt32();
                        if (line.Trim().StartsWith("ColumnStart")) ManiaSettings[currentKey].ColumnStart = v.ToInt32();
                        if (line.Trim().StartsWith("ColumnRight")) ManiaSettings[currentKey].ColumnRight = v.ToInt32();
                        if (line.Trim().StartsWith("ColumnSpacing")) ManiaSettings[currentKey].ColumnSpacing = v;
                        if (line.Trim().StartsWith("ColumnWidth")) ManiaSettings[currentKey].ColumnWidth = v;
                        if (line.Trim().StartsWith("ColumnLineWidth")) ManiaSettings[currentKey].ColumnLineWidth = v;
                        if (line.Trim().StartsWith("BarlineHeight"))
                            ManiaSettings[currentKey].BarlineHeight = v.ToDouble();
                        if (line.Trim().StartsWith("LightingNWidth")) ManiaSettings[currentKey].LightingNWidth = v;
                        if (line.Trim().StartsWith("LightingLWidth")) ManiaSettings[currentKey].LightingLWidth = v;
                        if (line.Trim().StartsWith("WidthForNoteHeightScale"))
                            ManiaSettings[currentKey].WidthForNoteHeightScale = v.ToNullableDouble();
                        if (line.Trim().StartsWith("HitPosition")) ManiaSettings[currentKey].HitPosition = v.ToInt32();
                        if (line.Trim().StartsWith("LightPosition"))
                            ManiaSettings[currentKey].LightPosition = v.ToInt32();
                        if (line.Trim().StartsWith("ScorePosition"))
                            ManiaSettings[currentKey].ScorePosition = v.ToNullableInt32();
                        if (line.Trim().StartsWith("ComboPosition"))
                            ManiaSettings[currentKey].ScorePosition = v.ToNullableInt32();
                        if (line.Trim().StartsWith("JudgementLine"))
                            ManiaSettings[currentKey].JudgementLine = v.ToInt32();
                        if (line.Trim().StartsWith("SpecialStyle"))
                        {
                            var suc = int.TryParse(v, out var style);
                            if (suc) ManiaSettings[currentKey].SpecialStyle = (SpecialStyles) style;
                            else ManiaSettings[currentKey].SpecialStyle = SkinTools.StringToEnum<SpecialStyles>(v);
                        }

                        if (line.Trim().StartsWith("ComboBurstStyle"))
                        {
                            var suc = int.TryParse(v, out var style);
                            if (suc) ManiaSettings[currentKey].ComboBurstStyle = (ComboBurstStyles) style;
                            else
                                ManiaSettings[currentKey].ComboBurstStyle = SkinTools.StringToEnum<ComboBurstStyles>(v);
                        }

                        if (line.Trim().StartsWith("SplitStages"))
                            ManiaSettings[currentKey].SplitStages = v.ToNullableBool();
                        if (line.Trim().StartsWith("StageSeparation"))
                            ManiaSettings[currentKey].StageSeparation = v.ToDouble();
                        if (line.Trim().StartsWith("KeysUnderNotes"))
                            ManiaSettings[currentKey].KeysUnderNotes = v.ToBool();
                        if (line.Trim().StartsWith("UpsideDown")) ManiaSettings[currentKey].UpsideDown = v.ToBool();
                        int currentColumn;
                        if (line.Trim().StartsWith("KeyFlipWhenUpsideDown"))
                        {
                            if (k.Last().IsDigit())
                            {
                                currentColumn = k.Last() - '0';
                                ManiaSettings[currentKey].KeyFlipWhenUpsideDown.SetForColumn(currentColumn, v.ToBool());
                            }
                            else if (k.Last() == 'D')
                            {
                                if (k[k.Length - 2].IsDigit())
                                {
                                    currentColumn = k.Last() - '0';
                                    ManiaSettings[currentKey].KeyFlipWhenUpsideDownD
                                        .SetForColumn(currentColumn, v.ToBool());
                                }
                            }
                            else
                            {
                                ManiaSettings[currentKey].KeyFlipWhenUpsideDown.SetForAllColumns(v.ToBool());
                            }
                        }

                        if (line.Trim().StartsWith("NoteFlipWhenUpsideDown"))
                        {
                            if (k.Last().IsDigit())
                            {
                                currentColumn = k.Last() - '0';
                                ManiaSettings[currentKey].NoteFlipWhenUpsideDown
                                    .SetForColumn(currentColumn, v.ToBool());
                            }
                            else if (k.Last() == 'H')
                            {
                                if (k[k.Length - 2].IsDigit())
                                {
                                    currentColumn = k.Last() - '0';
                                    ManiaSettings[currentKey].NoteFlipWhenUpsideDownH
                                        .SetForColumn(currentColumn, v.ToBool());
                                }
                                else
                                {
                                    throw new ArgumentException();
                                }
                            }
                            else if (k.Last() == 'L')
                            {
                                if (k[k.Length - 2].IsDigit())
                                {
                                    currentColumn = k.Last() - '0';
                                    ManiaSettings[currentKey].NoteFlipWhenUpsideDownL
                                        .SetForColumn(currentColumn, v.ToBool());
                                }
                                else
                                {
                                    throw new ArgumentException();
                                }
                            }
                            else if (k.Last() == 'T')
                            {
                                if (k[k.Length - 2].IsDigit())
                                {
                                    currentColumn = k.Last() - '0';
                                    ManiaSettings[currentKey].NoteFlipWhenUpsideDownT
                                        .SetForColumn(currentColumn, v.ToBool());
                                }
                                else
                                {
                                    throw new ArgumentException();
                                }
                            }
                        }

                        if (line.Trim().StartsWith("NoteBodyStyle"))
                        {
                            if (k.Last().IsDigit())
                            {
                                currentColumn = k.Last() - '0';
                                ManiaSettings[currentKey].NoteBodyStyle.SetForColumn(currentColumn, (HoldBodyStyle)v.ToInt32());
                            }
                            else
                            {
                                ManiaSettings[currentKey].NoteBodyStyle.SetForAllColumns((HoldBodyStyle)v.ToInt32());
                            }
                        }

                        if (line.Trim().StartsWith("Colour"))
                        {
                            var x = k.Replace("Colour", "");
                            if (k.Last().IsDigit())
                            {
                                currentColumn = k.Last() - '0';
                                ManiaSettings[currentKey].Color.SetForColumn(currentColumn, v.ToRgbaColor());
                            }
                            else if (k.StartsWith("Light") && x.Last().IsDigit())
                            {
                                currentColumn = k.Last() - '0';
                                ManiaSettings[currentKey].ColorLight.SetForColumn(currentColumn, v.ToRgbColor());
                            }
                            else if (k.EndsWith("Hold"))
                            {
                                ManiaSettings[currentKey].ColorHold = v.ToRgbaColor();
                            }
                            else if (k.EndsWith("ColumnLine"))
                            {
                                ManiaSettings[currentKey].ColorColumnLine = v.ToRgbaColor();
                            }
                            else if (k.EndsWith("JudgementLine"))
                            {
                                ManiaSettings[currentKey].ColorJudgementLine = v.ToRgbColor();
                            }
                            else if (k.EndsWith("KeyWarning"))
                            {
                                ManiaSettings[currentKey].ColorKeyWarning = v.ToRgbColor();
                            }
                            else if (k.EndsWith("Break"))
                            {
                                ManiaSettings[currentKey].ColorBreak = v.ToRgbaColor();
                            }
                            else
                            {
                                throw new ArgumentException();
                            }
                        }

                        if (line.Trim().StartsWith("KeyImage"))
                        {
                            if (k.Last().IsDigit())
                            {
                                currentColumn = k.Last() - '0';
                                ManiaSettings[currentKey].SkinImages.KeyImage
                                    .SetForColumn(currentColumn, new ManiaSkinImage(this, v));
                                //Debug.WriteLine("A Numbered Key Image detected.At line:\"" + line + "\" at column " + currentColumn.ToString());
                            }
                            else if (k.Last() == 'D')
                            {
                                if (k[k.Length - 2].IsDigit())
                                {
                                    currentColumn = k[k.Length - 2] - '0';
                                    //Debug.WriteLine("A Numbered Key Down Image detected.At line:\"" + line + "\" at column " + currentColumn.ToString());
                                    ManiaSettings[currentKey].SkinImages.KeyImageD
                                        .SetForColumn(currentColumn, new ManiaSkinImage(this, v));
                                }
                                else
                                {
                                    throw new ArgumentException();
                                }
                            }

                            //Debug.WriteLine("CurrentColumn returned to zero. CurrentColumn:"+currentColumn.ToString());
                        }

                        if (line.Trim().StartsWith("NoteImage"))
                        {
                            //Debug.WriteLine("Note Image detected.At line:\"" + line + "\"");
                            if (k.Last().IsDigit())
                            {
                                currentColumn = k.Last() - '0';
                                ManiaSettings[currentKey].SkinImages.NoteImage
                                    .SetForColumn(currentColumn, new ManiaSkinImage(this, v));
                            }
                            else if (k.Last() == 'H')
                            {
                                if (k[k.Length - 2].IsDigit())
                                {
                                    currentColumn = k[k.Length - 2] - '0';
                                    ManiaSettings[currentKey].SkinImages.NoteImageH
                                        .SetForColumn(currentColumn, new ManiaSkinImage(this, v));
                                }
                                else
                                {
                                    throw new ArgumentException();
                                }
                            }
                            else if (k.Last() == 'L')
                            {
                                if (k[k.Length - 2].IsDigit())
                                {
                                    currentColumn = k[k.Length - 2] - '0';
                                    ManiaSettings[currentKey].SkinImages.NoteImageL
                                        .SetForColumn(currentColumn, new ManiaSkinImage(this, v));
                                }
                                else
                                {
                                    throw new ArgumentException();
                                }
                            }
                            else if (k.Last() == 'T')
                            {
                                if (k[k.Length - 2].IsDigit())
                                {
                                    currentColumn = k[k.Length - 2] - '0';
                                    ManiaSettings[currentKey].SkinImages.NoteImageT
                                        .SetForColumn(currentColumn, new ManiaSkinImage(this, v));
                                }
                                else
                                {
                                    throw new ArgumentException();
                                }
                            }
                        }

                        if (line.Trim().StartsWith("StageLeft"))
                            ManiaSettings[currentKey].SkinImages.StageLeft = new ManiaSkinImage(this, v);
                        if (line.Trim().StartsWith("StageRight"))
                            ManiaSettings[currentKey].SkinImages.StageRight = new ManiaSkinImage(this, v);
                        if (line.Trim().StartsWith("StageHint"))
                            ManiaSettings[currentKey].SkinImages.StageHint = new ManiaSkinImage(this, v);
                        if (line.Trim().StartsWith("StageLight"))
                            ManiaSettings[currentKey].SkinImages.StageLight = new ManiaSkinImage(this, v);
                        if (line.Trim().StartsWith("LightingN"))
                            ManiaSettings[currentKey].SkinImages.LightingN = new ManiaSkinImage(this, v);
                        if (line.Trim().StartsWith("LightingL"))
                            ManiaSettings[currentKey].SkinImages.LightingL = new ManiaSkinImage(this, v);
                        if (line.Trim().StartsWith("WarningArrow"))
                            ManiaSettings[currentKey].SkinImages.WarningArrow = new ManiaSkinImage(this, v);
                        if (line.Trim().StartsWith("Hit0"))
                        {
                            SkinObjects.ManiaHitBurstImages.Hit0.Clear();
                            SkinObjects.ManiaHitBurstImages.Hit0.Add(new ManiaSkinImage(this, v));
                        }

                        if (line.Trim().StartsWith("Hit50"))
                        {
                            SkinObjects.ManiaHitBurstImages.Hit50.Clear();
                            SkinObjects.ManiaHitBurstImages.Hit50.Add(new ManiaSkinImage(this, v));
                        }

                        if (line.Trim().StartsWith("Hit100"))
                        {
                            SkinObjects.ManiaHitBurstImages.Hit100.Clear();
                            SkinObjects.ManiaHitBurstImages.Hit100.Add(new ManiaSkinImage(this, v));
                        }

                        if (line.Trim().StartsWith("Hit200"))
                        {
                            SkinObjects.ManiaHitBurstImages.Hit200.Clear();
                            SkinObjects.ManiaHitBurstImages.Hit200.Add(new ManiaSkinImage(this, v));
                        }

                        if (line.Trim().StartsWith("Hit300"))
                        {
                            SkinObjects.ManiaHitBurstImages.Hit300.Clear();
                            SkinObjects.ManiaHitBurstImages.Hit300.Add(new ManiaSkinImage(this, v));
                        }

                        if (line.Trim().StartsWith("Hit300g"))
                        {
                            SkinObjects.ManiaHitBurstImages.Hit300g.Clear();
                            SkinObjects.ManiaHitBurstImages.Hit300g.Add(new ManiaSkinImage(this, v));
                        }
                    }
                }
            }
        }
    }
}