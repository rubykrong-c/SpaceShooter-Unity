using UnityEngine;

namespace Code.Gameplay.Core.Ship
{
    public interface IShipPositionGetter
    {
        Vector2 PositionWeapon { get; }
    }
}