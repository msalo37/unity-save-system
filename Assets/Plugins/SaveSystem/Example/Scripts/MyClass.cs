using System;

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
