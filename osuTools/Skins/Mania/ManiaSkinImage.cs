using System.Drawing;
using System.IO;
using osuTools.Skins.Exceptions;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.SkinObjects.Mania
{
    public class ManiaSkinImage : ISkinImage
    {
        public ManiaSkinImage()
        {
        }

        public ManiaSkinImage(Skin parentSkin, string fileName, string skinImageTypeName)
        {
            var ini = parentSkin.ConfigFileDirectory;
            FileName = fileName;
            var tmppath = Path.GetDirectoryName(ini);
            FullPath = Path.Combine(tmppath, fileName);
            SkinImageTypeName = skinImageTypeName;
        }

        public ManiaSkinImage(string parentSkinDir, string fileName, string skinImageTypeName)
        {
            var ini = Path.GetDirectoryName(parentSkinDir);
            FileName = fileName;
            FullPath = ini;
            SkinImageTypeName = skinImageTypeName;
        }
        ///<inheritdoc/>
        public string FileName { get; }
        ///<inheritdoc/>
        public string FullPath { get; }
        ///<inheritdoc/>
        public string SkinImageTypeName { get; }
        ///<inheritdoc/>

        public Image LoadImage()
        {
            if (File.Exists(FullPath))
                return Image.FromFile(FullPath);
            throw new SkinFileNotFoundException();
        }
        ///<inheritdoc/>
        public ISkinImage GetHighResolutionImage()
        {
            var tmpname = FileName.Replace(".png", "@2x.png");
            var tmppath = Path.GetDirectoryName(FullPath);
            if (File.Exists(Path.Combine(tmppath, tmpname)))
                return new ManiaSkinImage(tmppath, tmpname, Path.Combine(tmppath, tmpname));
            throw new SkinFileNotFoundException("没有找到该皮肤文件的@2x版本。");
        }
    }
}