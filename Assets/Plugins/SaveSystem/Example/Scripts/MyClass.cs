using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MyClass
{
    public MyClass(string key)
    {
        this.key = key;
    }
    
    private string key;

    public string Key => key;
}
