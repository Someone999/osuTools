using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace osuTools.Beatmaps.HitObject
{
    /// <summary>
    ///     存储IHitObject的集合
    /// </summary>
    public class HitObjectCollection : List<IHitObject>
    {
        /// <summary>
        ///     连接两个谱面
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="breakTimeInMs"></param>
        /// <returns></returns>
        public static HitObjectCollection Contact(HitObjectCollection a, HitObjectCollection b, int breakTimeInMs = 0)
        {
            var beatmapoffset = a.Last().Offset;
            var c = new HitObjectCollection();
            foreach (var hitobject in a)
                c.Add(hitobject);
            foreach (var hitobject in b)
            {
                hitobject.Offset += breakTimeInMs + beatmapoffset;
                if (hitobject is ManiaHold)
                    (hitobject as ManiaHold).EndTime += beatmapoffset + breakTimeInMs;
                if (hitobject is Spinner)
                    (hitobject as Spinner).EndTime += beatmapoffset + breakTimeInMs;
                if (hitobject is BananaShower)
                    (hitobject as BananaShower).EndTime += beatmapoffset + breakTimeInMs;
                c.Add(hitobject);
            }

            return c;
        }

        /// <summary>
        ///     将集合中的所有IHitObject按照osu文件的格式写入指定文件
        /// </summary>
        /// <param name="file">要写入到的文件</param>
        /// <param name="overwrite">true将会用新的数据覆写文件，false则会在已有的数据后继续添加</param>
        public void WriteToFile(string file, bool overwrite = false)
        {
            var datas = new List<string>();
            foreach (var hitobject in this) datas.Add(hitobject.ToOsuFormat());
            if (!overwrite)
                File.AppendAllLines(file, datas.ToArray());
            else
                File.WriteAllLines(file, datas.ToArray());
        }
    }
}