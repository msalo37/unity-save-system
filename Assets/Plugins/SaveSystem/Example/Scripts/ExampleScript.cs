using System;
using System.Collections;
using System.Collections.Generic;
using msloo.SaveSystem;
using UnityEngine;
using UnityEngine.UI;

public class ExampleScript : MonoBehaviour
{
    [SerializeField] private InputField inputField;

    private void Awake()
    {
        SaveController.Init();

        if (SaveController.TryToGetSave("inputText", out string data))
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
        SaveController.SetSave("inputText" ,inputField.text);
    }

    public void Save()
    {
        SaveController.SaveAll();
    }

    public void Load()
    {
        inputField.text = SaveController.GetSave<string>("inputText");
    }

    public void DeleteSaves()
    {
        SaveController.DeleteAllSaves();
    }
}
