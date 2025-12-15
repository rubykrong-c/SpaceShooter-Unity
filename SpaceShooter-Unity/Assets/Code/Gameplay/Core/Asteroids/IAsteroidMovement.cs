namespace Code.Gameplay.Core
{
    public interface IAsteroidMovement
    {
        void Start(AsteroidContext context);
        void Tick(float deltaTime);
        void Stop();
    }
}