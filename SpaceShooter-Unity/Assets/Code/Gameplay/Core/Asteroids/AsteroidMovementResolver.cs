using System;
using System.Collections.Generic;
using Zenject;

namespace Code.Gameplay.Core
{
    public interface IAsteroidMovementResolver
    {
        IAsteroidMovement Resolve(EAsteroidType type);
    }

    public class AsteroidMovementResolver : IAsteroidMovementResolver
    {
        private readonly Dictionary<EAsteroidType, Func<IAsteroidMovement>> _map;

        private readonly Func<IAsteroidMovement> _defaultFactory;

        public AsteroidMovementResolver(
            LinearDownMovement.Factory linearFactory,
            SinusMovement.Factory sinusFactory)
        {
            _defaultFactory = () => linearFactory.Create();

            _map = new Dictionary<EAsteroidType, Func<IAsteroidMovement>>
            {
                { EAsteroidType.RED, _defaultFactory },
                { EAsteroidType.GRAY, () => sinusFactory.Create() },
            };
        }

        public IAsteroidMovement Resolve(EAsteroidType type)
        {
            return _map.TryGetValue(type, out var create)
                ? create()
                : _defaultFactory();
        }
    }
}