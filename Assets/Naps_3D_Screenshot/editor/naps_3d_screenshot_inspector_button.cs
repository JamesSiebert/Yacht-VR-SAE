using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(naps_3d_screen_cam_script))]
public class naps_3d_screen_inspector_button : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        naps_3d_screen_cam_script myScript = (naps_3d_screen_cam_script)target;

        if (Application.isPlaying)
        if (GUILayout.Button("Capture 3D Screenshot"))
        {
            Debug.Log("gui");
            Debug.Log(myScript);
            myScript.Capture();
        }
    }
}