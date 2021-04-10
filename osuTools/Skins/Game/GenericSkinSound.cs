using osuTools.Skins.Interfaces;

namespace osuTools.Skins.SkinObjects.Generic
{
    public class GenericSkinSound : ISkinSound
    {
        public GenericSkinSound(string fileName, string fullPath)
        {
            FileName = fileName;
            FullPath = fullPath;
            SkinSoundTypeName = fileName;
        }

        public string SkinSoundTypeName { get; internal set; } = "Default";
        public string FileName { get; internal set; } = "Default";
        public string FullPath { get; internal set; } = "Default";
    }
}