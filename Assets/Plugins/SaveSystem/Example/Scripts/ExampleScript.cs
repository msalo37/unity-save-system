using msloo.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

public class ExampleScript : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    private const string saveKey = "myClass";
    
    private void Awake()
    {
        SaveController.Init();

        if (SaveController.TryToGetSave(saveKey, out MyClass data))
            inputField.text = data.Key;
    }

    private void Start()
    {
        SaveController.onSaving += SaveText;
    }

    private void OnDestroy()
    {
        SaveController.onSaving -= SaveText;
    }

    private void SaveText()
    {
        SaveController.SetSave(saveKey ,new MyClass(inputField.text));
    }

    public void Save()
    {
        SaveController.SaveAll();
    }

    public void Load()
    {
        if (SaveController.TryToGetSave(saveKey, out MyClass data))
            inputField.text = data.Key;
        else
            inputField.text = "There is no saved data!";
    }

    public void DeleteSaves()
    {
        SaveController.DeleteAllSaves();
    }
}
