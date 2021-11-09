using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using msloo.SaveSystem.Settings;
using TMPro;
using UnityEngine;

namespace msloo.SaveSystem
{
    public class SaveController : MonoBehaviour
    {
        private static SaveController _instance;

        private static SaveController Instance
        {
            get
            {
                if (_instance == null)
                    CreateInstance();
                return _instance;
            }

            set => _instance = value;
        }

        private static void CreateInstance()
        {
            GameObject obj = new GameObject("Save system");
            _instance = obj.AddComponent<SaveController>();
            DontDestroyOnLoad(obj);
        }

        private Dictionary<string, Save> _saves = new Dictionary<string, Save>();
        private SaveSettings _settings;

        public static event Action onSaving;
        public static event Action onSavesLoaded;

        private void Awake()
        {
            _settings = Resources.Load<SaveSettings>("SaveSettings");
            
            if (_settings.autoSaveInterval != 0)
                StartCoroutine(AutoSave(_settings.autoSaveInterval));
        }

        private IEnumerator AutoSave(float sec)
        {
            while (true)
            {
                yield return new WaitForSeconds(sec);
                _SaveAll();
            }
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            if (_settings.saveOnApplicationPause == false) return;
            if (pauseStatus == false) return; // status == false means that game not hidden
            _SaveAll();
        }

        private void OnApplicationQuit()
        {
            if (_settings.saveOnApplicationQuit == false) return;
            _SaveAll();
        }

        private T _LoadSave<T>(string key)
        {
            if (_saves.ContainsKey(key))
                try
                {
                    return _saves[key].GetSavedObject<T>();
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }

            return default(T);
        }

        private void _SaveAll()
        {
            onSaving?.Invoke();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(_settings.GetPath(), FileMode.OpenOrCreate);
            bf.Serialize(file, _saves);
            file.Close();
        }

        private void _LoadAll()
        {
            if (File.Exists(_settings.GetPath()) == false)
                return;
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = new FileStream(_settings.GetPath(), FileMode.Open);

            try
            {
                _saves = (Dictionary<string, Save>) bf.Deserialize(file);
                onSavesLoaded?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError($"Saves file is not saves!\nError: {e.Message}");
            }
        }

        private void _DeleteAllSaves()
        {
            File.Delete(_settings.GetPath());
            _saves = new Dictionary<string, Save>();
        }
        
        public static void SaveAll()
        {
            Instance._SaveAll();
        }

        public static void SetSave<T>(string key, T data)
        {
            Instance._saves[key] = new Save(key, data);
        }

        public static bool TryToGetSave<T>(string key, out T data)
        {
            data = GetSave<T>(key);
            return Equals(data, default(T)) == false; // check if save is null
        }

        public static T GetSave<T>(string key)
        {
            return Instance._LoadSave<T>(key);
        }

        public static void Init()
        {
            Instance._LoadAll();
        }

        public static void DeleteAllSaves()
        {
            Instance._DeleteAllSaves();
        }
    }
}
