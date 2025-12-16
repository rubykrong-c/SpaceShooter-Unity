using System;
using Code.Gameplay;
using Code.Gameplay.Core;
using Code.Gameplay.Core.Ship;
using Code.Gameplay.Core.Ship.Bullet;
using UnityEngine;
using Zenject;
using IPoolable = Code.Base.Pool.IPoolable;


public class AsteroidBehaviour : MonoBehaviour, IPoolable
{
    [SerializeField] private SpriteRenderer _renderer;
    
    private IAsteroidMovement _movement;
    private Action<GameObject>  _onHit;
    
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
    
    public void SetCollisionHandler(Action<GameObject> onHit)
    {
        if (_onHit != null)
            return;

        _onHit = onHit;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BulletBehaviour>() != null
            || 
            other.GetComponent<ShipView>() != null)
        {
            _onHit?.Invoke(this.gameObject); 
        }
    }
}
