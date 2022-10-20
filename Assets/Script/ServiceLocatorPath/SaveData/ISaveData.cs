namespace SystemOfExtras.SavedData
{
    public interface ISaveData
    {
        void Save(string id, string data);
        string Get(string id);
    }
}