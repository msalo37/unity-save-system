using System.Collections;
using System.Collections.Generic;
using msloo.SaveSystem;
using UnityEngine;

namespace msloo.SaveSystem.Serializers
{
    public interface IDataLoader
    {
        void Serialize(Save[] data, string path);
        Save[] Deserialize(string path);
    }
}
