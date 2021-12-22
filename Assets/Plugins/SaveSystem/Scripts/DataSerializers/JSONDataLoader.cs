using System;
using System.IO;
using UnityEngine;

namespace msloo.SaveSystem.Serializers
{
    public class JSONDataLoader : MonoBehaviour, IDataLoader
    {
        public void Serialize(Save[] data, string path)
        {
            File.WriteAllText(path, JsonUtility.ToJson(data));
        }

        public Save[] Deserialize(string path)
        {
            try
            {
                return JsonUtility.FromJson<Save[]>(File.ReadAllText(path));
            }
            catch (Exception e)
            {
                Debug.LogError($"Saves file doesn't contain saves!\nError: {e.Message}");
                return null;
            }
        }
    }
}