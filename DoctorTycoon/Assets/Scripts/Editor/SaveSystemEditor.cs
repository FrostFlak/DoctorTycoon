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
        if (GUILayout.Button("Save Player Data"))
        {
            saveSystem.SavePlayerData();
        }
        else if (GUILayout.Button("Load Player Data"))
        {
            saveSystem.LoadPlayerData();
        }
        else if (GUILayout.Button("Reset Player Data"))
        {
            saveSystem.ResetPlayerData();
        }
        else if (GUILayout.Button("Delete PlayerData JSON"))
        {
            saveSystem.DeletePlayerDataSaveFiles();
        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Save Level Data"))
        {
            saveSystem.SaveLevelData();
        }
        else if (GUILayout.Button("Load Level Data"))
        {
            saveSystem.LoadLevelData();
        }
        else if (GUILayout.Button("Reset Level Data"))
        {
            saveSystem.ResetLevelData();
        }
        else if (GUILayout.Button("Delete Level JSON"))
        {
            saveSystem.DeleteLevelDataSaveFiles();
        }
        EditorGUILayout.Space();

        if (GUILayout.Button("Save Beds Data"))
        {
            saveSystem.SaveBedsData();
        }
        else if (GUILayout.Button("Load Beds Data"))
        {
            saveSystem.LoadBedsData();
        }
        else if (GUILayout.Button("Reset Beds Data"))
        {
            saveSystem.ResetBedsData();
        }
        else if (GUILayout.Button("Delete Beds JSON"))
        {
            saveSystem.DeleteBedsDataSaveFiles();
        }
    }
}
