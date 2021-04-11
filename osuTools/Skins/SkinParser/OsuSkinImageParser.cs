using System.IO;
using osuTools.Skins.SkinObjects.Osu;
using osuTools.Skins.Tools;

namespace osuTools.Skins
{
    public partial class Skin
    {
        private readonly string[] files = new string[0];

        private void getOsuSkinImage()
        {
            #region OsuRelatedImages

            var lst = SkinTools.GetMultipleFileSkinObject(files, "approachcircle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.ApproachCircle = new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "hitcircle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.HitCircle = new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "hitcircleselect");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.HitCircleSelect = new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "followpoint");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.FollowPoint = new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "reversearrow");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SliderSkinImages.ReverseArrow =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "sliderendcircle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SliderSkinImages.SliderEndCircle =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "sliderstartcircle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SliderSkinImages.SliderStartCircle =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "sliderscorepoint");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SliderSkinImages.SliderScorePoint =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "spinner-circle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerCircle =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "spinner-background");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerBackground =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "spinner-metre");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerMeter =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "spinner-bottom");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerBottom =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "spinner-glow");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerGlow =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "spinner-middle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerMiddle =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "spinner-middle2");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerMiddle2 =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "spinner-top");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerTop =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "spinner-approachcircle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerApproachCircle =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "spinner-clear");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerClear =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "spinner-spin");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerSpin =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(files, "spinner-rpm");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerRPM =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region OsuRelatedOverlay

            lst = SkinTools.GetMultipleFileSkinObject(files, "hitcircleoverlay");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitCircleOverlay.Add(new OsuSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(files, "sliderstartcircleoverlay");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.SliderSkinImages.SliderStartCircleOverlay.Add(
                        new OsuSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(files, "sliderendcircleoverlay");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.SliderSkinImages.SliderEndCircleOverlay.Add(
                        new OsuSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(files, "sliderb");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.SliderSkinImages.SliderBall.Add(new OsuSkinImage(Path.GetFileName(file),
                        file));

            lst = SkinTools.GetMultipleFileSkinObject(files, "sliderfollowcircle");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.SliderSkinImages.SliderFollowCircle.Add(
                        new OsuSkinImage(Path.GetFileName(file), file));

            #endregion

            #region HitBurst

            lst = SkinTools.GetMultipleFileSkinObject(files, "hit300");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitBurstImages.Hit300.Add(new OsuSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(files, "hit100");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitBurstImages.Hit100.Add(new OsuSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(files, "hit50");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitBurstImages.Hit50.Add(new OsuSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(files, "hit0");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitBurstImages.Hit0.Add(new OsuSkinImage(Path.GetFileName(file), file));
            lst = SkinTools.GetMultipleFileSkinObject(files, "hit300k");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitBurstImages.Hit300k.Add(new OsuSkinImage(Path.GetFileName(file),
                        file));
            lst = SkinTools.GetMultipleFileSkinObject(files, "hit100k");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitBurstImages.Hit100k.Add(new OsuSkinImage(Path.GetFileName(file),
                        file));

            #endregion
        }
    }
}