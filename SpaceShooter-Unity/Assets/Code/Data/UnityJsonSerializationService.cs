using System;
using UnityEngine;

namespace Code
{
    public class UnityJsonSerializationService : ISerializationService
    {
        public string Serialize(object obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public object Deserialize(string json, Type type)
        {
            return JsonUtility.FromJson(json, type);
        }
    }
}