using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveSystem))]
public class SaveSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SaveSystem saveSystem = (SaveSystem)target;
        if (GUILayout.Button("Save Data"))
        {
            saveSystem.SaveGame();
        }
        else if (GUILayout.Button("Load Data"))
        {
            saveSystem.LoadGame();
        }
        else if (GUILayout.Button("Reset Data"))
        {
            saveSystem.ResetData();
        }
        else if (GUILayout.Button("Delete JSON"))
        {
            saveSystem.DeleteSaveFile();
        }
    }
}
