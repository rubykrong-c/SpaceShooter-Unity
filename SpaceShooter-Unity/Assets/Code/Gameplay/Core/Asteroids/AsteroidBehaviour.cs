using Code.Base.Pool;
using Code.Gameplay.Core;
using UnityEngine;


public class AsteroidBehaviour : MonoBehaviour, IPoolable
{
    [SerializeField] private SpriteRenderer _renderer;
    
    public EAsteroidType Type { get; private set; }
    
    public void Configure(EAsteroidType colorId, Color color)
    {
        Type =  colorId;
        if (_renderer != null )
        {
            _renderer.color = color;
        }
    }

    public void Initilize()
    {
    }

    public void Dispose()
    {
    }
}
