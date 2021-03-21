namespace osuTools.Beatmaps.Classes.StoryBoard
{
    public class StringProcessor
    {
        public string OrignalString { get; private set; }
        public string ProcessedString { get; private set; }
        public int SpaceNum { get; private set; }
        public virtual string Process()
        {
            string x = "";
            x += OrignalString;
            while (x.StartsWith(" "))
            {
                x = x.Remove(0, 1);
                SpaceNum++;
            }
            ProcessedString = x;
            return ProcessedString;
        }
        public StringProcessor(string s)
        {
            OrignalString += s;
        }
    }
}