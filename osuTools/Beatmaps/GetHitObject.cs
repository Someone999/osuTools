using System.Collections.Generic;
using System.IO;
using osuTools.Beatmaps.HitObject;
using osuTools.Game.Modes;

namespace osuTools.Beatmaps
{
    public partial class Beatmap
    {
        private void GetHitObjects()
        {
            //Stopwatch t = new Stopwatch();
            // t.Start();
            var block = DataBlock.None;
            var objects = new HitObjectCollection();
            if (string.IsNullOrEmpty(FullPath))
                _hitObjects = objects;
            var map = File.ReadAllLines(FullPath);
            foreach (var str in map)
            {
                if (str.Contains("[HitObjects]"))
                {
                    block = DataBlock.HitObjects;
                    continue;
                }

                if (block == DataBlock.HitObjects)
                {
                    var comasp = str.Split(',');
                    if (comasp.Length > 4)
                    {
                        objects.Add(Mode == OsuGameMode.Mania
                            ? GameMode.FromLegacyMode(Mode).CreateHitObject(str, (int) CircleSize)
                            : GameMode.FromLegacyMode(Mode).CreateHitObject(str));
                    }
                }
            }

            _hitObjects = objects;
            // t.Stop();

            //IO.CurrentIO.Write($"Read HitObjects:{objects.Count} Time:{t.Elapsed.TotalSeconds}s");
        }

        /// <summary>
        ///     获取指定类型的打击物件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>包含所有符合要求的打击物件的列表</returns>
        public List<T> GetHitObjects<T>()
        {
            var listT = new List<T>();
            foreach (var hitObject in HitObjects)
                if (hitObject is T obj)
                    listT.Add(obj);
            return listT;
        }
    }
}