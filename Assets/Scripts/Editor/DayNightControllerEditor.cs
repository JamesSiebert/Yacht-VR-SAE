using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DayNightController))]
public class DayNightControllerEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DayNightController dayNightController = (DayNightController)target;
        
        if(GUILayout.Button("Enable Day (lights & Sounds)"))
        {
            dayNightController.EnableDay();
        }
        
        if(GUILayout.Button("Enable Night (lights & Sounds)"))
        {
            dayNightController.EnableNight();
        }
        
        if(GUILayout.Button("Toggle Engine Sounds"))
        {
            dayNightController.ToggleEngineSounds();
        }
        
        if(GUILayout.Button("Toggle Stereo Sounds"))
        {
            dayNightController.ToggleStereoSounds();
        }
        
        if(GUILayout.Button("Jetski Down"))
        {
            dayNightController.JetskiDown();
        }
        
        if(GUILayout.Button("Jetski Up"))
        {
            dayNightController.JetskiUp();
        }
        
        if(GUILayout.Button("Table Down"))
        {
            dayNightController.TableDown();
        }
        
        if(GUILayout.Button("Table Up"))
        {
            dayNightController.TableUp();
        }
    }
}