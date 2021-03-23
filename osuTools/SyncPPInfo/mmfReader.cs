using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace osuTools
{
    partial class SyncPPInfo
    {
        private int a = 0;
        private readonly ORTDPWrapper ot;
        private string PPILine, PPLine, HitLine, TimeLine, ComboLine;
        private MemoryMappedViewStream PPInfomfsStream;
        private readonly byte[] RawData = new byte[4097];
        private double stars;
        private string[] strarr;


        public void GetInfo(object sender, EventArgs e)
        {
            var size = File.Open("C:\\Users\\add.DESKTOP-RQ91ME4\\Desktop\\sync app\\plugins\\formatconfig.ini",
                FileMode.Open).Length;

            #region 从mmf获取信息

            try
            {
                PPInfomfsStream = PPInfomfs.CreateViewStream();
                // System.Windows.Forms.MessageBox.Show(PPInfomfsStream.Length.ToString());
                PPInfomfsStream.Read(RawData, 0, (int) PPInfomfsStream.Length);
                RawStr = Encoding.Default.GetString(RawData).Split('\0')[0];

                #endregion

                #region 获取星星数

                strarr = RawStr.Split('\n');
                if (ot.GameMode.CurrentMode == OsuGameMode.Mania)
                {
                    double.TryParse(File.ReadAllText("D:\\osu\\stars.txt"), out stars);
                }

                #endregion

                #region PP的组成

                PPILine = strarr[0];
                var ppitemp = PPILine.Split('/');
                double.TryParse(ppitemp[0], out accpp);
                double.TryParse(ppitemp[1], out aimpp);
                double.TryParse(ppitemp[2], out speedpp);
                double.TryParse(ppitemp[3], out fcaim);
                double.TryParse(ppitemp[4], out fcacc);
                double.TryParse(ppitemp[5], out fcspeed);
                double.TryParse(ppitemp[6], out maccpp);
                double.TryParse(ppitemp[7], out maimpp);
                double.TryParse(ppitemp[8], out mspeedpp);

                #endregion

                #region PP

                PPLine = strarr[1];
                var pptemp = PPLine.Split('/');
                double.TryParse(pptemp[0], out rpp);
                double.TryParse(pptemp[1], out fpp);
                double.TryParse(pptemp[2], out mpp);

                #endregion

                #region 时间操作(以毫秒为单位)

                TimeLine = strarr[2];
                var timetemp = TimeLine.Split('/');
                double.TryParse(timetemp[0], out du);
                double.TryParse(timetemp[1], out pt);
                if (pt > du) du = pt;
                d = TimeSpan.FromMilliseconds(du);
                s = TimeSpan.FromMilliseconds(pt);
                timep = pt / du;

                #endregion

                #region 判定结果

                HitLine = strarr[3];
                var hittemp = HitLine.Split('/');
                int.TryParse(hittemp[0], out C300g);
                int.TryParse(hittemp[1], out C300);
                int.TryParse(hittemp[2], out C200);
                int.TryParse(hittemp[3], out C100);
                int.TryParse(hittemp[4], out C50);
                int.TryParse(hittemp[5], out Cmiss);

                #endregion

                #region 连击与note数量

                ComboLine = strarr[4];
                var comtemp = ComboLine.Split('/');
                int.TryParse(comtemp[0], out fc);
                int.TryParse(comtemp[1], out objcount);
                int.TryParse(comtemp[2], out maxc);
                int.TryParse(comtemp[3], out pmaxc);
                int.TryParse(comtemp[4], out cc);
                var sqq = "shit!";
                MemoryMappedFile.CreateOrOpen("Shit", 100).CreateViewStream()
                    .Write(Encoding.Default.GetBytes(sqq), 0, sqq.Length);

                #endregion

                #region 对字符串进行格式化

                FormatedHitStr =
                    $"{c300}x300 {c100}x100 {c50}x50 {cMiss}xMiss\n{c300g}x300g({(c300g / (c300g + c300)).ToString("p")}) {c200}x200";

                FormatedTimeStr =
                    $"{CurrentTime.Minutes.ToString("d2")}:{CurrentTime.Seconds.ToString("d2")}/{SongDuration.Minutes.ToString("d2")}:{SongDuration.Seconds.ToString("d2")}";
                FormatedPPStr = $"{CurrentPP.ToString("f2")}pp / {FcPP.ToString("f2")}pp => {MaxPP.ToString("f2")}pp";
                var f = MemoryMappedFile.CreateOrOpen("Fuck", 999);
                var c = "Fucked";
                f.CreateViewStream().Write(Encoding.Default.GetBytes(c), 0, c.Length);
                var size2 = File.Open("C:\\Users\\add.DESKTOP-RQ91ME4\\Desktop\\sync app\\plugins\\formatconfig.ini",
                    FileMode.Open).Length;
                if (size != size2)
                {
                }

                #endregion
            }
            catch (Exception x)
            {
                File.AppendAllText("D:\\osu\\osuTools\\log\\0.txt", x.ToString());
            }
        }
    }

    /*我刚刚试了一下32767->32768的经验值*/
}