using System.IO;
using osuTools.Skins.Mania;

namespace osuTools.Skins
{
    public partial class Skin
    {
        private void GetManiaSkinImages()
        {
            #region ManiaHitBurstImages

            var lst = SkinTools.GetMultipleFileSkinObject(_files, "mania-hit300g");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaHitBurstImages.Hit300g.Add(new ManiaSkinImage(this, Path.GetFileName(file)));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "mania-hit300");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaHitBurstImages.Hit300.Add(new ManiaSkinImage(this, Path.GetFileName(file)));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "mania-hit200");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaHitBurstImages.Hit200.Add(new ManiaSkinImage(this, Path.GetFileName(file)));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "mania-hit100");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaHitBurstImages.Hit100.Add(new ManiaSkinImage(this, Path.GetFileName(file)));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "mania-hit50");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaHitBurstImages.Hit50.Add(new ManiaSkinImage(this, Path.GetFileName(file)));

            lst = SkinTools.GetMultipleFileSkinObject(_files, "mania-hit0");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaHitBurstImages.Hit0.Add(new ManiaSkinImage(this, Path.GetFileName(file)));
            lst = SkinTools.GetMultipleFileSkinObject(_files, "comboburst-mania-");
            if (lst.Count > 0)
                foreach (var file in lst)
                    SkinObjects.ManiaComboBurstImages.ComboBurstImages.Add(new ManiaSkinImage(this,Path.GetFileName(file)));

            #endregion
        }
    }
}