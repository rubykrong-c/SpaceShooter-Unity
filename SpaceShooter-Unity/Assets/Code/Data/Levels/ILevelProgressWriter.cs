namespace Code.Levels
{
    public interface ILevelProgressWriter
    {
        void CompleteCurrentLevelAndGenerateNext();
    }
}