using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization;

namespace msloo.SaveSystem
{
    [Serializable]
    public class Save
    {
        public Save(string key, object savedObject)
        {
            this.key = key;
            this.savedObject = savedObject;
            this.date = DateTime.Now;
        }

        public readonly string key;
        private object savedObject;
        private DateTime date;

        public T GetSavedObject<T>()
        {
            return (T)savedObject;
        }
    }
}