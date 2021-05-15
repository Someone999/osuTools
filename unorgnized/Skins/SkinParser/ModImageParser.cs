using System.IO;
using System.Linq;
using osuTools.Skin.Mods;

namespace osuTools.Skins
{
    public partial class Skin
    {
        private void GetModsImages()
        {
            var s = Directory.GetFiles(ConfigFileDirectory.Replace("skin.ini", ""), "*.png",
                SearchOption.TopDirectoryOnly);
            foreach (var filedir in s)
            {
                var filename = Path.GetFileName(filedir);
                if (filename.StartsWith("selection-mod-"))
                {
                    var str = filename.Split('-').Last().Replace(".png", "");
                    if (str == "nofail") SkinObjects.ModImages.NoFail = new ModImage(filename, filedir);
                    if (str == "easy") SkinObjects.ModImages.Easy = new ModImage(filename, filedir);
                    if (str == "halftime") SkinObjects.ModImages.HalfTime = new ModImage(filename, filedir);
                    if (str == "hardrock") SkinObjects.ModImages.HardRock = new ModImage(filename, filedir);
                    if (str == "suddendeath") SkinObjects.ModImages.SuddenDeath = new ModImage(filename, filedir);
                    if (str == "perfect") SkinObjects.ModImages.Perfect = new ModImage(filename, filedir);
                    if (str == "doubletime") SkinObjects.ModImages.DoubleTime = new ModImage(filename, filedir);
                    if (str == "nightcore") SkinObjects.ModImages.NightCore = new ModImage(filename, filedir);
                    if (str == "fadein") SkinObjects.ModImages.FadeIn = new ModImage(filename, filedir);
                    if (str == "hidden") SkinObjects.ModImages.Hidden = new ModImage(filename, filedir);
                    if (str == "flashlight") SkinObjects.ModImages.Flashlight = new ModImage(filename, filedir);
                    if (str == "key1") SkinObjects.ModImages.Key1 = new ModImage(filename, filedir);
                    if (str == "key2") SkinObjects.ModImages.Key2 = new ModImage(filename, filedir);
                    if (str == "key3") SkinObjects.ModImages.Key3 = new ModImage(filename, filedir);
                    if (str == "key4") SkinObjects.ModImages.Key4 = new ModImage(filename, filedir);
                    if (str == "key5") SkinObjects.ModImages.Key5 = new ModImage(filename, filedir);
                    if (str == "key6") SkinObjects.ModImages.Key6 = new ModImage(filename, filedir);
                    if (str == "key7") SkinObjects.ModImages.Key7 = new ModImage(filename, filedir);
                    if (str == "key8") SkinObjects.ModImages.Key8 = new ModImage(filename, filedir);
                    if (str == "key9") SkinObjects.ModImages.Key9 = new ModImage(filename, filedir);
                    if (str == "keycoop") SkinObjects.ModImages.KeyCoop = new ModImage(filename, filedir);
                    if (str == "relax") SkinObjects.ModImages.Relax = new ModImage(filename, filedir);
                    if (str == "autopilot") SkinObjects.ModImages.AutoPilot = new ModImage(filename, filedir);
                    if (str == "spunout") SkinObjects.ModImages.SpunOut = new ModImage(filename, filedir);
                    if (str == "random") SkinObjects.ModImages.Random = new ModImage(filename, filedir);
                    if (str == "mirror") SkinObjects.ModImages.Mirror = new ModImage(filename, filedir);
                    if (str == "autoplay") SkinObjects.ModImages.AutoPlay = new ModImage(filename, filedir);
                    if (str == "cinema") SkinObjects.ModImages.Cinema = new ModImage(filename, filedir);
                    if (str == "scorev2") SkinObjects.ModImages.ScoreV2 = new ModImage(filename, filedir);
                }
            }
        }
    }
}