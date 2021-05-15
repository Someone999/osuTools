using System.IO;
using osuTools.Skins.SkinObjects.Generic;
using osuTools.Skins.SkinObjects.Generic.Rank;
using osuTools.Skins.Tools;

namespace osuTools.Skins
{
    /// <summary>
    /// 游戏的皮肤
    /// </summary>
    public partial class Skin
    {
        private void GetGenericSkinImage()
        {
            #region Background

            var lst = SkinTools.GetMultipleFileSkinObject(_files, "menu-background");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.MenuBackground = new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region Cursor

            lst = SkinTools.GetMultipleFileSkinObject(_files, "cursor");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.Cursor = new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "cursortrail");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.CursorTrail = new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region HitCircleNumber

            lst = SkinTools.GetMultipleFileSkinObject(_files, "default-");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.GenericSkinObjects.HitCircleNumberImages.Add(
                        new GenericSkinImage(Path.GetFileName(file), file));

            #endregion

            #region Score

            lst = SkinTools.GetMultipleFileSkinObject(_files, "Score-dot");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ScoreImages.Dot = new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "Score-comma");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ScoreImages.Coma =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "Score-percent");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ScoreImages.Percent =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "score-x");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ScoreImages.x = new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "default-");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.GenericSkinObjects.HitCircleNumberImages.Add(
                        new GenericSkinImage(Path.GetFileName(file), file));
            lst = SkinTools.GetMultipleFileSkinObject(_files, "Score-");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.GenericSkinObjects.ScoreImages.ScoreNumbers.Add(
                        new GenericSkinImage(Path.GetFileName(file), file));

            #endregion

            #region MenuBack

            lst = SkinTools.GetMultipleFileSkinObject(_files, "menu-back-");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.GenericSkinObjects.MenuBackImages.Add(
                        new GenericSkinImage(Path.GetFileName(file), file));

            #endregion

            #region MenuButtonBackground

            lst = SkinTools.GetMultipleFileSkinObject(_files, "menu-button-background");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.MenuButtonBackground =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region MenuSnow

            lst = SkinTools.GetMultipleFileSkinObject(_files, "menu-snow");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.MenuSnow = new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region Star

            lst = SkinTools.GetMultipleFileSkinObject(_files, "star");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.Star = new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region ModeList

            lst = SkinTools.GetMultipleFileSkinObject(_files, "mode-osu-med");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ModeListImages.Osu =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "mode-taiko-med");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ModeListImages.Taiko =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "mode-fruits-med");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ModeListImages.Catch =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "mode-mania-med");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ModeListImages.Mania =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region Skip

            lst = SkinTools.GetMultipleFileSkinObject(_files, "play-skip-");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.GenericSkinObjects.SkipImages.Add(new GenericSkinImage(Path.GetFileName(file), file));

            #endregion

            #region RankingImage

            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-X");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.RankingImages.SS = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-XH");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.RankingImages.SSH = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-S");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.RankingImages.S = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-SH");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.RankingImages.SH = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-A");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.RankingImages.A = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-B");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.RankingImages.B = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-C");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.RankingImages.C = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-D");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.RankingImages.D = new RankingImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region ResultPage

            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-accuracy");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ResultPageImages.Accuracy =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-panel");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ResultPageImages.Panel =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-graph");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ResultPageImages.TimePerformanceBox =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-perfect");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ResultPageImages.Perfect =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-maxcombo");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ResultPageImages.MaxCombo =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-title");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ResultPageImages.Title =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "pause-retry");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ResultPageImages.Retry =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "pause-replay");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ResultPageImages.Replay =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-retry");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ResultPageImages.Retry =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region PauseMenu

            lst = SkinTools.GetMultipleFileSkinObject(_files, "pause-retry");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.PauseMenuImages.Retry =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "pause-back");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.PauseMenuImages.Back =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "pause-continue");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.PauseMenuImages.Continue =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region ReadyCountdown

            lst = SkinTools.GetMultipleFileSkinObject(_files, "ready");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.Countdown.Ready.Image =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "count1");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.Countdown.One.Image =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "count2");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.Countdown.Two.Image =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "count3");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.Countdown.Three.Image =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "go");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.Countdown.Go.Image =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "readys");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.Countdown.Ready.Sound =
                    new GenericSkinSound(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "count1s");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.Countdown.One.Sound =
                    new GenericSkinSound(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "count2s");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.Countdown.Two.Sound =
                    new GenericSkinSound(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "count3s");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.Countdown.Three.Sound =
                    new GenericSkinSound(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "gos");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.Countdown.Go.Sound =
                    new GenericSkinSound(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region ScoreBar

            lst = SkinTools.GetMultipleFileSkinObject(_files, "scorebar-bg");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ScoreBarSkinImages.ScoreBarBackgorund =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ready");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.GenericSkinObjects.ScoreBarSkinImages.ScoreBarColour.Add(
                        new GenericSkinImage(Path.GetFileName(file), file));
            lst = SkinTools.GetMultipleFileSkinObject(_files, "scorebar-ki");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ScoreBarSkinImages.ScoreBarKi =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "scorebar-kidanger");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ScoreBarSkinImages.ScoreBarKiDanger =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "scorebar-kidanger2");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ScoreBarSkinImages.ScoreBarKiCritical =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "scorebar-marker");
            if (lst.Count > 0)
                SkinObjects.GenericSkinObjects.ScoreBarSkinImages.ScoreBarMarker =
                    new GenericSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion
        }
    }
}