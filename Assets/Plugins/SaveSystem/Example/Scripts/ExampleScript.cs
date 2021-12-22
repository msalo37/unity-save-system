using msloo.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

public class ExampleScript : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    private const string saveKey = "myClass";
    
    private void Awake()
    {
        if (SaveController.TryToGetSave(saveKey, out string data))
            inputField.text = data;
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
        SaveController.SetSave(saveKey ,inputField.text);
    }

    public void Save()
    {
        SaveController.SaveAll();
    }

    public void Load()
    {
        if (SaveController.TryToGetSave(saveKey, out string data))
            inputField.text = data;
        else
            inputField.text = "There is no saved data!";
    }

    public void DeleteSaves()
    {
        SaveController.DeleteAllSaves();
    }
}
