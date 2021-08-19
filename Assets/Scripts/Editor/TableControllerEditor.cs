using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TableController))]
public class TableControllerEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TableController tableController = (TableController)target;

        if(GUILayout.Button("Table Down"))
        {
            tableController.TableDown();
        }
        
        if(GUILayout.Button("Table Up"))
        {
            tableController.TableUp();
        }
    }
}