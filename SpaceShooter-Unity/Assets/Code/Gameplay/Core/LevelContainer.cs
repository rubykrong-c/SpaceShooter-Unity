using System.Collections.Generic;
using UnityEngine;

public class LevelContainer : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();

    public List<Vector2> GetSpawnPointsPos()
    {
        List<Vector2> list = new List<Vector2>();

        foreach (var spawn in _spawnPoints)
        {
            list.Add(spawn.position);
        }

        return list;
    }
}
