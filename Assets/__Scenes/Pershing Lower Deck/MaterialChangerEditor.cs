using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MaterialChanger))]
public class MaterialChangerEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MaterialChanger materialChanger = (MaterialChanger)target;
        
        // Changes all wood materials
        EditorGUILayout.HelpBox("Changes the wood material in PLAY mode", MessageType.Info);
        for (int i = 0; i < materialChanger.woodMaterials.Length; i++)
        {
            if(GUILayout.Button("Change Wood - " + i.ToString()))
            {
                materialChanger.ChangeWood(i);
            }
        }
        
        // Changes all bed sheet materials
        EditorGUILayout.HelpBox("Changes the bed sheets in PLAY mode", MessageType.Info);
        for (int i = 0; i < materialChanger.bedSheetMaterials.Length; i++)
        {
            if(GUILayout.Button("Change Bed Sheets - " + i.ToString()))
            {
                materialChanger.ChangeBedSheets(i);
            }
        }
        
    }
}