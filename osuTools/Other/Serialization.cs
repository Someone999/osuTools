namespace osuTools
{
    internal interface IJsonSerilizable
    {
        void Serialize(string file);
        void Deserialize(string file);
    }
}