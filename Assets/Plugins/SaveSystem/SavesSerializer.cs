using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using msloo.SaveSystem.Settings;
using UnityEngine;

namespace msloo.SaveSystem
{
    public class SavesSerializer
    {
        public SavesSerializer(SaveSettings settings)
        {
            _settings = settings;
            LoadDictionary();
        }

        private SaveSettings _settings;
        private Dictionary<string, Save> _savesDictionary;

        private void LoadDictionary()
        {
            if (File.Exists(_settings.GetPath()) == false)
            {
                _savesDictionary = new Dictionary<string, Save>();
                return;
            }
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(_settings.GetPath(), FileMode.Open);

            try
            {
                _savesDictionary = (Dictionary<string, Save>) bf.Deserialize(file);
            }
            catch (Exception e)
            {
                Debug.LogError($"Saves file is not dictionary!\nError: {e.Message}");
            }
        }
        
        public T GetSave<T>(string key) where T : class
        {
            if (_savesDictionary.ContainsKey(key))
                try
                {
                    return _savesDictionary[key].GetSavedObject<T>();
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }

            return null;
        }
        
        public void SetSave<T>(string key, T data) where T : class
        {
            _savesDictionary[key] = new Save(key, data);
        } 

        public void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(_settings.GetPath(), FileMode.OpenOrCreate);
            bf.Serialize(file, _savesDictionary);
            file.Close();
        }

        public void RemoveSave(string key)
        {
            _savesDictionary.Remove(key);
        }

        public void RemoveSaves()
        {
            _savesDictionary = new Dictionary<string, Save>();
            File.Delete(_settings.GetPath());
            Save();
        }
    }
}
