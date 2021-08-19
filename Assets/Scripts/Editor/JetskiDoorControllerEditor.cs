using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(JetskiDoorController))]
public class JetskiDoorControllerEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        JetskiDoorController tableController = (JetskiDoorController)target;

        if(GUILayout.Button("Jetski Down"))
        {
            tableController.JetskiDown();
        }
        
        if(GUILayout.Button("Jetski Up"))
        {
            tableController.JetskiUp();
        }
    }
}