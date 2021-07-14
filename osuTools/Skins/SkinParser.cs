using System.IO;
using osuTools.Skins.Game;
using osuTools.Skins.Game.Rank;

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
                SkinObjects.GeneralSkinObjects.MenuBackground = new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region Cursor

            lst = SkinTools.GetMultipleFileSkinObject(_files, "cursor");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.Cursor = new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "cursortrail");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.CursorTrail = new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region HitCircleNumber

            lst = SkinTools.GetMultipleFileSkinObject(_files, "default-");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.GeneralSkinObjects.HitCircleNumberImages.Add(
                        new GeneralSkinImage(Path.GetFileName(file), file));

            #endregion

            #region Score

            lst = SkinTools.GetMultipleFileSkinObject(_files, "Score-dot");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ScoreImages.Dot = new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "Score-comma");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ScoreImages.Coma =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "Score-percent");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ScoreImages.Percent =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "score-x");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ScoreImages.x = new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "default-");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.GeneralSkinObjects.HitCircleNumberImages.Add(
                        new GeneralSkinImage(Path.GetFileName(file), file));
            lst = SkinTools.GetMultipleFileSkinObject(_files, "Score-");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.GeneralSkinObjects.ScoreImages.ScoreNumbers.Add(
                        new GeneralSkinImage(Path.GetFileName(file), file));

            #endregion

            #region MenuBack

            lst = SkinTools.GetMultipleFileSkinObject(_files, "menu-back-");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.GeneralSkinObjects.MenuBackImages.Add(
                        new GeneralSkinImage(Path.GetFileName(file), file));

            #endregion

            #region MenuButtonBackground

            lst = SkinTools.GetMultipleFileSkinObject(_files, "menu-button-background");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.MenuButtonBackground =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region MenuSnow

            lst = SkinTools.GetMultipleFileSkinObject(_files, "menu-snow");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.MenuSnow = new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region Star

            lst = SkinTools.GetMultipleFileSkinObject(_files, "star");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.Star = new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region ModeList

            lst = SkinTools.GetMultipleFileSkinObject(_files, "mode-osu-med");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ModeListImages.Osu =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "mode-taiko-med");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ModeListImages.Taiko =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "mode-fruits-med");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ModeListImages.Catch =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "mode-mania-med");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ModeListImages.Mania =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region Skip

            lst = SkinTools.GetMultipleFileSkinObject(_files, "play-skip-");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.GeneralSkinObjects.SkipImages.Add(new GeneralSkinImage(Path.GetFileName(file), file));

            #endregion

            #region RankingImage

            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-X");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.RankingImages.SS = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-XH");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.RankingImages.SSH = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-S");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.RankingImages.S = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-SH");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.RankingImages.SH = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-A");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.RankingImages.A = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-B");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.RankingImages.B = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-C");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.RankingImages.C = new RankingImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-D");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.RankingImages.D = new RankingImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region ResultPage

            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-accuracy");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ResultPageImages.Accuracy =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-panel");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ResultPageImages.Panel =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-graph");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ResultPageImages.TimePerformanceBox =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-perfect");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ResultPageImages.Perfect =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-maxcombo");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ResultPageImages.MaxCombo =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-title");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ResultPageImages.Title =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "pause-retry");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ResultPageImages.Retry =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "pause-replay");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ResultPageImages.Replay =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ranking-retry");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ResultPageImages.Retry =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region PauseMenu

            lst = SkinTools.GetMultipleFileSkinObject(_files, "pause-retry");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.PauseMenuImages.Retry =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "pause-back");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.PauseMenuImages.Back =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "pause-continue");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.PauseMenuImages.Continue =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region ReadyCountdown

            lst = SkinTools.GetMultipleFileSkinObject(_files, "ready");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.Countdown.Ready.Image =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "count1");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.Countdown.One.Image =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "count2");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.Countdown.Two.Image =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "count3");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.Countdown.Three.Image =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "go");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.Countdown.Go.Image =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "readys");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.Countdown.Ready.Sound =
                    new GeneralSkinSound(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "count1s");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.Countdown.One.Sound =
                    new GeneralSkinSound(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "count2s");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.Countdown.Two.Sound =
                    new GeneralSkinSound(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "count3s");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.Countdown.Three.Sound =
                    new GeneralSkinSound(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "gos");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.Countdown.Go.Sound =
                    new GeneralSkinSound(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region ScoreBar

            lst = SkinTools.GetMultipleFileSkinObject(_files, "scorebar-bg");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ScoreBarSkinImages.ScoreBarBackgorund =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "ready");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.GeneralSkinObjects.ScoreBarSkinImages.ScoreBarColour.Add(
                        new GeneralSkinImage(Path.GetFileName(file), file));
            lst = SkinTools.GetMultipleFileSkinObject(_files, "scorebar-ki");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ScoreBarSkinImages.ScoreBarKi =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "scorebar-kidanger");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ScoreBarSkinImages.ScoreBarKiDanger =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "scorebar-kidanger2");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ScoreBarSkinImages.ScoreBarKiCritical =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);
            lst = SkinTools.GetMultipleFileSkinObject(_files, "scorebar-marker");
            if (lst.Count > 0)
                SkinObjects.GeneralSkinObjects.ScoreBarSkinImages.ScoreBarMarker =
                    new GeneralSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion
        }
    }
}