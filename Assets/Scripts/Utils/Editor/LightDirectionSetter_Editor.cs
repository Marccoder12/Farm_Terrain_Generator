using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightDirectionSetter))]
public class LightDirectionSetter_Editor : Editor
{
        
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        LightDirectionSetter myScript = (LightDirectionSetter) target;
        
        if(myScript.ShowLightVec) {
            myScript.SetLightDir();
        }
    }
}
