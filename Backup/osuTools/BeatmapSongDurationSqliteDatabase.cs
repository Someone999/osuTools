using System.Collections.Generic;
using System.Data.SQLite;
using Newtonsoft.Json;

namespace osuTools
{
    /// <summary>
    /// 存储<seealso cref="BeatmapSongDurationSqliteDatabase"/>的集合
    /// </summary>
    public class BeatmapSongDurationSqliteDatabase
    {
        private List<BeatmapSongDuration> Durations { get; } = new List<BeatmapSongDuration>();

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; }
        /// <summary>
        /// 使用文件路径初始化<seealso cref="BeatmapSongDurationSqliteDatabase"/>
        /// </summary>
        /// <param name="file"></param>
        public BeatmapSongDurationSqliteDatabase(string file)
        {
            SQLiteConnection connection = new SQLiteConnection($"Datasource = {file}");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "select * from SongDuration";
            FilePath = file;
            var reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Durations.Add(
                            new BeatmapSongDuration(
                                int.Parse(reader["BeatmapId"].ToString()),
                                reader["Md5"].ToString(),
                            double.Parse(reader["Duration"].ToString())));
                    }
                }
            }
            catch (SQLiteException)
            {
            }
            finally
            {
                connection.Close();
            }
           
        }
        /// <summary>
        /// <inheritdoc cref="List{T}.Add"/>
        /// 与<seealso cref="List{T}"/>不同的是，这个列表不允许添加重复项
        /// </summary>
        public void Add(BeatmapSongDuration duration,bool updateExisted = false)
        {
            if (!Durations.Contains(duration))
            {
                Durations.Add(duration);
                SQLiteConnection connection = new SQLiteConnection($"Datasource = {FilePath}");
                connection.Open();
                var existedCommand = connection.CreateCommand();
                var insertCommand = connection.CreateCommand();
                existedCommand.CommandText =
                    $"select exists (select * from SongDuration where \"Md5\" = '{duration.BeatmapMd5}') == 1 as good";
                insertCommand.CommandText =
                    $"insert into SongDuration values({duration.BeatmapId},'{duration.BeatmapMd5}',{duration.Duration})";
                var reader = existedCommand.ExecuteReader();
                if(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader["good"].ToString() == "0")
                            insertCommand.ExecuteNonQuery();
                    }
                }
            }
            else if(updateExisted)
            {
                int idx = Durations.IndexOf(duration);
                if(idx == -1)
                    Add(duration);
                else
                {
                    BeatmapSongDuration b = Durations[idx];
                    b.Duration = duration.Duration;
                    SQLiteConnection connection = new SQLiteConnection($"Datasource = {FilePath}");
                    var updateCommand = connection.CreateCommand();
                    updateCommand.CommandText =
                        $"update SongDuration set Duration = {duration.Duration} where \"Md5\" = '{duration.BeatmapMd5}'";
                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// <inheritdoc cref="List{T}.Remove"/>
        /// </summary>
        /// <param name="duration"></param>
        public void Remove(BeatmapSongDuration duration)
        {
            Durations.Remove(duration);
            SQLiteConnection connection = new SQLiteConnection($"Datasource = {FilePath}");
            var deleteCommand = connection.CreateCommand();
            deleteCommand.CommandText = $"delete SongDuration where \"Md5\" = '{duration.BeatmapMd5}'";
        }

        /// <summary>
        /// 通过BeatmapMd5获取<seealso cref="BeatmapSongDuration"/>，找不到则返回null
        /// </summary>
        /// <param name="beatmapMd5"></param>
        /// <returns></returns>
        public BeatmapSongDuration GetDurationByBeatmapMd5(string beatmapMd5) =>
            Durations.Find(dur => dur.BeatmapMd5 == beatmapMd5);
        /// <summary>
        /// 通过BeatmapId和BeatmapMd5设置对应的<seealso cref="BeatmapSongDuration"/>，BeatmapId可以只在需要添加的情况下填写有效值
        /// </summary>
        /// <param name="beatmapId"></param>
        /// <param name="beatmapMd5"></param>
        /// <param name="millisec"></param>
        public void SetDurationByBeatmapMd5(int beatmapId,string beatmapMd5, int millisec)
        {
            Add(new BeatmapSongDuration(beatmapId, beatmapMd5, millisec));
        }
        

    }
}
