using Code.Gameplay.Core;
using UnityEngine;
using Zenject;
using IPoolable = Code.Base.Pool.IPoolable;


public class AsteroidBehaviour : MonoBehaviour, IPoolable
{
    [SerializeField] private SpriteRenderer _renderer;
    private IAsteroidMovement _movement;
    
    public EAsteroidType Type { get; private set; }
    
    public void Configure(EAsteroidType colorId, AsteroidSpawner.AsteroidData data, IAsteroidMovement movement)
    {
        Type =  colorId;
        if (_renderer != null )
        {
            _renderer.color = data.ColorAsteroid;
        }

        _movement = movement;
        _movement.Start(new AsteroidContext(transform, data.Speed));
    }

    public void Initilize()
    {
    }

    public void Dispose()
    {
        _movement?.Stop();
    }
    
    private void Update()
    {
        _movement?.Tick(Time.deltaTime);
    }

}
