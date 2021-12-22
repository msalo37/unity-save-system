using System;

namespace msloo.SaveSystem
{
    [Serializable]
    public class Save
    {
        public Save(string key, object savedObject)
        {
            this.key = key;
            this.savedObject = savedObject;
            this.lastTimeSaved = DateTime.Now;
        }

        public readonly string key;
        public object savedObject;
        public DateTime lastTimeSaved;
    }
}