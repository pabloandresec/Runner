using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(Pick))]
public class PickCI : Editor
{
    private Pick tgt;

    private void OnEnable()
    {
        tgt = target as Pick;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
