namespace Code
{
    public interface ISerializationService
    {
        string Serialize(object obj);
        object Deserialize(string json, System.Type type);
    }
}