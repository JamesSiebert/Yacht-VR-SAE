using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Which script we are customising
[CustomEditor(typeof(LoginManager))]

public class LoginManagerEditorScript : Editor
{
    public override void OnInspectorGUI(){
        EditorGUILayout.HelpBox("This script is responsible for connecting to Photon Servers.", MessageType.Info);

        LoginManager loginManager = (LoginManager)target;
        if(GUILayout.Button("Connect Anonymously")){
            loginManager.ConnectToPhotonServer();
        }

    }
    
}
