using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace msloo.SaveSystem.DataHolders
{
    public interface IDataHolder
    {
        object GetData(string key);
        bool TryToGetData(string key, out object data);
        void SetData(string key, object data);
        void RemoveData(string key);
        void RemoveAllData();
        Save[] GetSaveArray();
        void LoadSaveArray(Save[] arr);
    }
}
