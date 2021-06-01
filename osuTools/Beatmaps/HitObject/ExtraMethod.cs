namespace osuTools.Beatmaps.HitObject
{
    /// <summary>
    /// HitObject的扩展方法
    /// </summary>
    public static class HitObjectExtraMethod
    {
        /// <summary>
        /// 获取一个打击物件的结束时间，没有结束时间的取开始时间
        /// </summary>
        /// <param name="hitObject"></param>
        /// <returns></returns>
        public static int GetEndTime(this IHitObject hitObject)
        {
            if (hitObject is null) return 0;
            if (hitObject is IHasEndHitObject hasEnd)
                return hasEnd.EndTime;
            return hitObject.Offset;
        }
    }
}
