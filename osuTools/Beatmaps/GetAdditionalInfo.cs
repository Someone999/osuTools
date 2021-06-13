using System;
using osuTools.Beatmaps.HitObject.Sounds;
using osuTools.Exceptions;
using osuTools.Skins;

namespace osuTools.Beatmaps
{
    partial class Beatmap
    {
        private void getAddtionalInfo(string[] dataPares)
        {
            foreach (var d in dataPares)
                try
                {
                    var data = d.Split(':');
                    if (data[0].Contains("AudioLeadIn:"))
                    {
                        double.TryParse(data[1].Trim(), out var val);
                        AudioLeadIn = val;
                        continue;
                    }

                    if (data[0].Contains("PreviewTime:"))
                    {
                        double.TryParse(data[1].Trim(), out var val);
                        PreviewTime = val;
                        continue;
                    }

                    if (data[0].Contains("Countdown:"))
                    {
                        int.TryParse(data[1].Trim(), out var val);
                        HasCountdown = val.ToBool();
                        continue;
                    }

                    if (data[0].Contains("SampleSet:"))
                    {
                        if (int.TryParse(data[1].Trim(), out var val))
                            SampleSet = (SampleSets) val;
                        else
                            SampleSet = SkinTools.StringToEnum<SampleSets>(data[1].Trim());
                        continue;
                    }

                    if (data[0].Contains("StackLeniency:"))
                    {
                        double.TryParse(data[1].Trim(), out var val);
                        StackLeniency = val;
                        continue;
                    }

                    if (data[0].Contains("LetterboxInBreaks:"))
                    {
                        int.TryParse(data[1].Trim(), out var val);
                        LetterboxInBreaks = val.ToBool();
                        continue;
                    }

                    if (data[0].Contains("WidescreenStoryboard"))
                    {
                        int.TryParse(data[1].Trim(), out var val);
                        WidescreenStoryboard = val.ToBool();
                        continue;
                    }

                    if (data[0].Contains("Bookmarks"))
                    {
                        var bookmarks = data[1].Trim();
                        if (bookmarks == "0")
                            return;
                        var offsets = bookmarks.Split(',');
                        foreach (var offset in offsets)
                            try
                            {
                                var innerOffset = int.Parse(offset);
                                Bookmarks.Add(innerOffset);
                            }
                            catch (Exception ex)
                            {
                                throw new FailToParseException("无法通过Bookmarks标签获取书签。", ex);
                            }

                        continue;
                    }

                    if (data[0].Contains("DistanceSpacing"))
                    {
                        double.TryParse(data[1].Trim(), out var val);
                        DistanceSpacing = val;
                        continue;
                    }

                    if (data[0].Contains("BeatDivisor"))
                    {
                        double.TryParse(data[1].Trim(), out var val);
                        BeatDivisor = val;
                        continue;
                    }

                    if (data[0].Contains("GridSize"))
                    {
                        double.TryParse(data[1].Trim(), out var val);
                        GridSize = val;
                        continue;
                    }

                    if (data[0].Contains("TimelineZoom"))
                    {
                        double.TryParse(data[1].Trim(), out var val);
                        TimelineZoom = val;
                        continue;
                    }

                    if (data[0].Contains("SliderMultiplier"))
                    {
                        double.TryParse(data[1].Trim(), out var val);
                        SliderMultiplier = val;
                        continue;
                    }

                    if (data[0].Contains("SliderTickRate"))
                    {
                        double.TryParse(data[1].Trim(), out var val);
                        SliderTickRate = val;
                    }
                }
                catch (Exception ex)
                {
                    throw new FailToParseException("从谱面文件获取信息失败。", ex);
                }
        }
    }
}