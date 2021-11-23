using System;
using System.Collections;
using msloo.SaveSystem.Settings;
using UnityEngine;

namespace msloo.SaveSystem
{
    public class SaveController : MonoBehaviour
    {
        #region Signleton

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

        #endregion

        private SaveSettings _settings;
        private SavesSerializer _serializer;
        
        public static event Action onSaving;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else if (_instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _settings = Resources.Load<SaveSettings>("SaveSettings");
            _serializer = new SavesSerializer(_settings);
            
            if (_settings.autoSaveInterval != 0)
                StartCoroutine(AutoSave(_settings.autoSaveInterval));
        }

        private IEnumerator AutoSave(float sec)
        {
            while (true)
            {
                yield return new WaitForSeconds(sec);
                onSaving?.Invoke();
                _serializer.Save();
            }
        }
        
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus == false) return; // status == false means that game opened
            if (_settings.saveOnApplicationPause == false) return;
            _serializer.Save();
        }

        private void OnApplicationQuit()
        {
            if (_settings.saveOnApplicationQuit == false) return;
            _serializer.Save();
        }

        public static void SaveAll()
        {
            onSaving?.Invoke();
            Instance._serializer.Save();
        }

        public static void SetSave<T>(string key, T data) where T : class
        {
            Instance._serializer.SetSave(key, data);
        }

        public static bool TryToGetSave<T>(string key, out T data) where T : class
        {
            data = GetSave<T>(key);
            return data != null;
        }

        public static T GetSave<T>(string key) where T : class
        {
            return Instance._serializer.GetSave<T>(key);
        }

        public static void Init()
        { 
            if (_instance == null)
                CreateInstance();
        }

        public static void DeleteAllSaves()
        {
            Instance._serializer.RemoveSaves();
        }
    }
}
