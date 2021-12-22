using msloo.SaveSystem;
using msloo.SaveSystem.DataHolders;
using msloo.SaveSystem.Serializers;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveController))]
public class SaveControllerEditor : Editor
{
    private SaveController script;
    private GameObject gameObject;

    private string dataLoader = nullData;
    private string dataHolder = nullData;
    
    private const string nullData = "NULL";

    private GUIStyle textStyle;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        EditorGUILayout.LabelField("You need to add \"IDataLoader\" and \"IDataHolder\" components to this GameObject!", textStyle);
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("DataLoader: " + dataLoader);
        EditorGUILayout.LabelField("DataHolder: " + dataHolder);
    }

    private void Awake()
    {
        textStyle = EditorStyles.label;
        textStyle.wordWrap = true;
    }

    private void Reset()
    {
        script = (SaveController) target;
        gameObject = script.gameObject;
        
        dataLoader = gameObject.TryGetComponent(out IDataLoader _dataLoaderScript)
            ? _dataLoaderScript.GetType().Name
            : nullData;
        
        dataHolder = gameObject.TryGetComponent(out IDataHolder _dataLHolderScript)
            ? _dataLHolderScript.GetType().Name
            : nullData;
    }
}
