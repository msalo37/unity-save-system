using UnityEngine;

namespace msloo.SaveSystem.Settings
{
    [CreateAssetMenu(fileName = "SaveSettings", menuName = "ScriptableObjects/SaveSystem/SaveSettings")]
    public class SaveSettings : ScriptableObject
    {
        [SerializeField]
        private SavePath path = SavePath.PersistentData;
        
        [SerializeField]
        private string filename = "main.save";
        
        [Header("Save game when game is closing")]
        [Tooltip("MonoBehaviour.OnApplicationQuit()")]
        public bool saveOnApplicationQuit = true;
        
        [Header("Save the game when the game is pausing")]
        [Tooltip("MonoBehaviour.OnApplicationPause(bool)")]
        public bool saveOnApplicationPause = false;
        
        [Header("Auto save interval (seconds)")]
        [Tooltip("if you don't want auto save to work, set this parameter to zero")]
        public float autoSaveInterval;

        private string GetSavePath()
        {
            switch (path)
            {
                case SavePath.Data :
                    return Application.dataPath;
                
                case SavePath.PersistentData :
                    return Application.persistentDataPath;
                
                case SavePath.StreamingAssets :
                    return Application.streamingAssetsPath;
            }

            return string.Empty;
        }

        public string GetPath()
        {
            return System.IO.Path.Combine(GetSavePath(), filename);
        }
    }

    public enum SavePath
    {
        PersistentData,
        Data,
        StreamingAssets
    }
}
