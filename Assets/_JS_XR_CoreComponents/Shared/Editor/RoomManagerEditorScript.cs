using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.HelpBox("This script is responsible for creating and joining rooms", MessageType.Info);

        RoomManager roomManager = (RoomManager)target;

        if(GUILayout.Button("Join Training Room"))
        {
            roomManager.OnEnterRoomButtonClicked_Training();
        }
        
        if(GUILayout.Button("Join Pershing Island Room"))
        {
            roomManager.OnEnterRoomButtonClicked_PershingIsland();
        }
        
        if(GUILayout.Button("Join Pershing Ocean Room"))
        {
            roomManager.OnEnterRoomButtonClicked_PershingOcean();
        }
    }
}
