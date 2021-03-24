using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteCutterWindowData))]
public class SpriteCutterWindowDataCI : Editor
{
    SpriteCutterWindowData tgt;

    private void OnEnable()
    {
        tgt = target as SpriteCutterWindowData;
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Process..."))
        {
            tgt.ProcessSprite();
        }
    }
}