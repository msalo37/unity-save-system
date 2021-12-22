using System;
using System.Collections;
using msloo.SaveSystem.DataHolders;
using msloo.SaveSystem.Serializers;
using msloo.SaveSystem.Settings;
using UnityEngine;

namespace msloo.SaveSystem
{
    public class SaveController : MonoBehaviour
    {
        public static SaveController Instance
        {
            private set;
            get;
        }

        private SaveSettings _settings;
        private IDataLoader _serializer;
        private IDataHolder _dataHolder;

        public static event Action onSaving;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
                
            _settings = Resources.Load<SaveSettings>("SaveSettings");
            _serializer = GetComponent<IDataLoader>();
            _dataHolder = GetComponent<IDataHolder>();
            
            _dataHolder.LoadSaveArray(_serializer.Deserialize(_settings.GetPath()));
        }

        private void Start()
        {
            if (_settings.autoSaveInterval != 0)
                StartCoroutine(AutoSave(_settings.autoSaveInterval));
        }

        private IEnumerator AutoSave(float sec)
        {
            while (true)
            {
                yield return new WaitForSeconds(sec);
                SaveAll();
            }
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus == false) return; // status == false means that game opened
            if (_settings.saveOnApplicationPause == false) return;
            SaveAll();
        }
        
        private void OnApplicationQuit()
        {
            if (_settings.saveOnApplicationQuit == false) return;
            SaveAll();
        }

        public static void SaveAll()
        {
            onSaving?.Invoke();
            Instance._serializer.Serialize(Instance._dataHolder.GetSaveArray(), Instance._settings.GetPath());
        }

        public static void SetSave<T>(string key, T data)
        {
            Instance._dataHolder.SetData(key, data);
        }

        public static bool TryToGetSave<T>(string key, out T data)
        {
            bool b = Instance._dataHolder.TryToGetData(key, out object d);
            data = (T) d;
            return b;
        }

        public static T GetSave<T>(string key)
        {
            return (T)Instance._dataHolder.GetData(key);
        }

        public static void DeleteAllSaves()
        {
            Instance._dataHolder.RemoveAllData();
        }

        public static void DeleteSave(string key)
        {
            Instance._dataHolder.RemoveData(key);
        }
    }
}
