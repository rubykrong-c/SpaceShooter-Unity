using System.IO;
using UnityEngine;

namespace Code
{
    public class FileStorageService : IStorageService
    {
        private readonly string _folderPath;

        public FileStorageService()
        {
            _folderPath = Path.Combine(UnityEngine.Application.persistentDataPath, "Saves");
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
        }

        public void Save(string key, string json)
        {
            var path = GetPath(key);
            File.WriteAllText(path, json);
        }

        public bool TryLoad(string key, out string json)
        {
            var path = GetPath(key);
            if (File.Exists(path))
            {
                json = File.ReadAllText(path);
                return true;
            }

            json = null;
            return false;
        }

        private string GetPath(string key)
        {
            return Path.Combine(_folderPath, key + ".json");
        }
    }
}