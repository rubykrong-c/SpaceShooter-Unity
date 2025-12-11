using System.IO;
using UnityEditor;
using UnityEngine;

namespace Code.Editor
{
    public static class SaveSystemTools
    {
        private const string MENU_PATH = "SaveSystem/Show in Finder";

        [MenuItem(MENU_PATH, priority = 0)]
        public static void ShowSaveFolder()
        {
            var path = UnityEngine.Application.persistentDataPath;

            if (!Directory.Exists(path))
            {
                Debug.LogWarning($"Save directory does not exist: {path}");
                return;
            }

            EditorUtility.RevealInFinder(path);
        }
    }
}

