using System.Collections.Generic;
using System.Linq;
using msloo.SaveSystem;
using msloo.SaveSystem.DataHolders;
using UnityEngine;

public class DictionaryDataHolder : MonoBehaviour, IDataHolder
{
    private Dictionary<string, Save> _dictionary;

    private void Awake()
    {
        _dictionary ??= new Dictionary<string, Save>();
    }

    public object GetData(string key)
    {
        if (_dictionary.ContainsKey(key))
            return _dictionary[key].savedObject;

        return null;
    }

    public bool TryToGetData(string key, out object data)
    {
        data = GetData(key);
        return data != null;
    }

    public void SetData(string key, object data)
    {
        _dictionary[key] = new Save(key, data);
    }

    public void RemoveData(string key)
    {
        if (_dictionary.ContainsKey(key))
            _dictionary.Remove(key);
    }

    public void RemoveAllData()
    {
        _dictionary = new Dictionary<string, Save>();
    }

    public Save[] GetSaveArray()
    {
        return _dictionary.Values.ToArray();
    }

    public void LoadSaveArray(Save[] arr)
    {
        if (arr == null) return;
        
        _dictionary = new Dictionary<string, Save>();
        
        foreach (Save save in arr)
            if (save != null)
                _dictionary[save.key] = save;
    }
}
