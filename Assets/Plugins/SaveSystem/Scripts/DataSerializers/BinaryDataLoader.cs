using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace msloo.SaveSystem.Serializers
{
    public class BinaryDataLoader : MonoBehaviour, IDataLoader
    {
        public void Serialize(Save[] data, string path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.OpenOrCreate);
            bf.Serialize(file, data);
            file.Close();
        }

        public Save[] Deserialize(string path)
        {
            if (File.Exists(path) == false)
                return null;
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);

            try
            {
                return (Save[])bf.Deserialize(file);
            }
            catch (Exception e)
            {
                Debug.LogError($"Saves file doesn't contain saves!\nError: {e.Message}");
                return null;
            }
        }
    }
}
