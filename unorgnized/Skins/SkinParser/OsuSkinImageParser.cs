using System.IO;
using osuTools.Skins.SkinObjects.Osu;
using osuTools.Skins.Tools;

namespace osuTools.Skins
{
    public partial class Skin
    {
        private readonly string[] _files = new string[0];

        private void GetOsuSkinImage()
        {
            #region OsuRelatedImages

            var lst = SkinTools.GetMultipleFileSkinObject(_files, "approachcircle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.ApproachCircle = new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "hitcircle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.HitCircle = new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "hitcircleselect");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.HitCircleSelect = new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "followpoint");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.FollowPoint = new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "reversearrow");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SliderSkinImages.ReverseArrow =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "sliderendcircle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SliderSkinImages.SliderEndCircle =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "sliderstartcircle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SliderSkinImages.SliderStartCircle =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "sliderscorepoint");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SliderSkinImages.SliderScorePoint =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "spinner-circle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerCircle =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "spinner-background");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerBackground =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "spinner-metre");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerMeter =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "spinner-bottom");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerBottom =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "spinner-glow");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerGlow =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "spinner-middle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerMiddle =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "spinner-middle2");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerMiddle2 =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "spinner-top");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerTop =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "spinner-approachcircle");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerApproachCircle =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "spinner-clear");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerClear =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "spinner-spin");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerSpin =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "spinner-rpm");
            if (lst.Count > 0)
                SkinObjects.OsuSkinImages.SpinnerSkinImages.SpinnerRPM =
                    new OsuSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region OsuRelatedOverlay

            lst = SkinTools.GetMultipleFileSkinObject(_files, "hitcircleoverlay");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitCircleOverlay.Add(new OsuSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "sliderstartcircleoverlay");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.SliderSkinImages.SliderStartCircleOverlay.Add(
                        new OsuSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "sliderendcircleoverlay");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.SliderSkinImages.SliderEndCircleOverlay.Add(
                        new OsuSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "sliderb");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.SliderSkinImages.SliderBall.Add(new OsuSkinImage(Path.GetFileName(file),
                        file));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "sliderfollowcircle");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.SliderSkinImages.SliderFollowCircle.Add(
                        new OsuSkinImage(Path.GetFileName(file), file));

            #endregion

            #region HitBurst

            lst = SkinTools.GetMultipleFileSkinObject(_files, "hit300");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitBurstImages.Hit300.Add(new OsuSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "hit100");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitBurstImages.Hit100.Add(new OsuSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "hit50");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitBurstImages.Hit50.Add(new OsuSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "hit0");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitBurstImages.Hit0.Add(new OsuSkinImage(Path.GetFileName(file), file));
            lst = SkinTools.GetMultipleFileSkinObject(_files, "hit300k");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitBurstImages.Hit300k.Add(new OsuSkinImage(Path.GetFileName(file),
                        file));
            lst = SkinTools.GetMultipleFileSkinObject(_files, "hit100k");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.OsuSkinImages.HitBurstImages.Hit100k.Add(new OsuSkinImage(Path.GetFileName(file),
                        file));

            #endregion
        }
    }
}