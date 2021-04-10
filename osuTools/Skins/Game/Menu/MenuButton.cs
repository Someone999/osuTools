using System.IO;
using osuTools.Skins.Exceptions;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.SkinObjects.Generic.Menu
{
    public class MenuButton : GenericSkinImage
    {
        public MenuButton(string fileName, string fullFileName) : base(fileName, fullFileName)
        {
            FileName = fileName;
            var type = fileName.Replace(".png", "");
            FullPath = fullFileName;
        }

        /// <summary>
        ///     获取鼠标悬浮在按钮上时的图片皮肤元素
        /// </summary>
        /// <returns></returns>
        public ISkinImage GetMouseOverImage()
        {
            var tmpname = FileName.Replace(".png", "-over.png");
            var tmppath = Path.GetDirectoryName(FullPath);
            if (File.Exists(Path.Combine(tmppath, tmpname)))
                return new GenericSkinImage(tmpname, Path.Combine(tmppath, tmpname));
            throw new SkinFileNotFoundException();
        }
    }
}