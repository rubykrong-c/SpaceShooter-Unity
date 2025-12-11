namespace Code
{
    public interface IStorageService
    {
        void Save(string key, string json);
        bool TryLoad(string key, out string json);
    }
}