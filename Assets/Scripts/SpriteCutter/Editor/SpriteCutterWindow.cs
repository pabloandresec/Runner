using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteCutterWindow : ExtendedEditor
{
    public SpriteCutterWindowData scriptableObject;

    public static void ShowWindow(SpriteCutterWindowData data)
    {
        SpriteCutterWindow window = GetWindow<SpriteCutterWindow>("SpriteCutter");
        window.serializedObject = new SerializedObject(data);
        window.scriptableObject = data;
    }

    private void OnGUI()
    {
        GUILayout.Label("Simple tool that does the following:\n" +
            "1.Cut a texture in sprites by specified columns and rows\n" +
            "2.Create animations with those sprites\n" +
            "3.Apply them to an animator Override\n" +
            "4.Saving all the data in the resources folder based on the set name", EditorStyles.boldLabel);
        currentProperty = serializedObject.FindProperty("settings");
        DrawProperties(currentProperty, true);

        if(GUILayout.Button("Process Sprite"))
        {
            scriptableObject.ProcessSprite();
        }
    }
}
