using System;
using System.IO;
using osuTools.Exceptions;
using osuTools.Skins.Interfaces;

namespace osuTools.Skins.Game.Menu
{
    /// <summary>
    /// 菜单按钮的图像
    /// </summary>
    public class MenuButton : GeneralSkinImage
    {
        /// <summary>
        /// 使用指定的文件名和全路径初始化MenuButton
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullFileName"></param>
        public MenuButton(string fileName, string fullFileName) : base(fileName, fullFileName)
        {
            FileName = fileName;
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
            if (File.Exists(Path.Combine(tmppath ?? throw new InvalidOperationException(), tmpname)))
                return new GeneralSkinImage(tmpname, Path.Combine(tmppath, tmpname));
            throw new SkinFileNotFoundException();
        }
    }
}