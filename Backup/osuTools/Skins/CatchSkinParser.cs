using System.IO;
using osuTools.Skins.Catch;

namespace osuTools.Skins
{
    public partial class Skin
    {
        private void GetCatchSkinImage()
        {
            #region CatchFruitImage

            var lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-apple");
            if (lst.Count > 0)
                SkinObjects.CatchSkinImages.Fruit.Apple = new CatchSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-grapes");
            if (lst.Count > 0)
                SkinObjects.CatchSkinImages.Fruit.Grapes = new CatchSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-orange");
            if (lst.Count > 0)
                SkinObjects.CatchSkinImages.Fruit.Orange = new CatchSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-pear");
            if (lst.Count > 0)
                SkinObjects.CatchSkinImages.Fruit.Pear = new CatchSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-bananas");
            if (lst.Count > 0)
                SkinObjects.CatchSkinImages.Fruit.Bananas = new CatchSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-drop");
            if (lst.Count > 0)
                SkinObjects.CatchSkinImages.Fruit.Drop = new CatchSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion

            #region FruitCatcherImage

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-catcher-idle");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.CatchSkinImages.FruitCatcher.Idle.Add(new CatchSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-catcher-fail");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.CatchSkinImages.FruitCatcher.Fail.Add(new CatchSkinImage(Path.GetFileName(file), file));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-catcher-kiai");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.CatchSkinImages.FruitCatcher.Kiai.Add(new CatchSkinImage(Path.GetFileName(file), file));

            #endregion

            #region CatchFruitOverlay

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-apple-overlay");
            if (lst.Count > 0)
                SkinObjects.CatchSkinImages.Fruit.AppleOverlay = new CatchSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-pear-overlay");
            if (lst.Count > 0)
                SkinObjects.CatchSkinImages.Fruit.PearOverlay = new CatchSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-grapes-overlay");
            if (lst.Count > 0)
                SkinObjects.CatchSkinImages.Fruit.GrapesOverlay = new CatchSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-orange-overlay");
            if (lst.Count > 0)
                SkinObjects.CatchSkinImages.Fruit.OrangeOverlay = new CatchSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-bananas-overlay");
            if (lst.Count > 0)
                SkinObjects.CatchSkinImages.Fruit.BananasOverlay = new CatchSkinImage(Path.GetFileName(lst[0]), lst[0]);

            lst = SkinTools.GetMultipleFileSkinObject(_files, "fruit-drop-overlay");
            if (lst.Count > 0)
                SkinObjects.CatchSkinImages.Fruit.DropOverlay = new CatchSkinImage(Path.GetFileName(lst[0]), lst[0]);

            #endregion
        }
    }
}