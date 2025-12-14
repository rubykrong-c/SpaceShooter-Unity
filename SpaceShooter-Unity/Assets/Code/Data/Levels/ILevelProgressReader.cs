namespace Code.Levels
{
    public interface ILevelProgressReader
    {
        int CurrentLevel { get;}

        LevelParams GetParamCurrentLevel();
    }
}