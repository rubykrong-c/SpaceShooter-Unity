using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class SaveManager : ISaveManager
    {
        private readonly List<ISaveable> _saveables;
        private readonly IStorageService _storage;
        private readonly ISerializationService _serializer;

        public SaveManager(
            List<ISaveable> saveables,
            IStorageService storage,
            ISerializationService serializer)
        {
            _saveables = saveables;
            _storage = storage;
            _serializer = serializer;
        }

        public void SaveAll()
        {
            foreach (var saveable in _saveables)
            {
                try
                {
                    var state = saveable.CaptureState();
                    var json = _serializer.Serialize(state);
                    _storage.Save(saveable.SaveKey, json);
                }
                catch (Exception e)
                {
                    Debug.LogError($"[SaveManager] Failed to save '{saveable.SaveKey}': {e}");
                }
            }
        }

        public void LoadAll()
        {
            foreach (var saveable in _saveables)
            {
                try
                {
                    if (_storage.TryLoad(saveable.SaveKey, out var json))
                    {
                        var data = _serializer.Deserialize(json, saveable.DataType);
                        saveable.RestoreState(data);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"[SaveManager] Failed to load '{saveable.SaveKey}': {e}");
                }
            }
        } 
    }
}