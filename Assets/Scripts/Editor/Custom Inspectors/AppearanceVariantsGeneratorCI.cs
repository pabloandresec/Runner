using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AppearanceVariantsGenerator))]
public class AppearanceVariantsGeneratorCI : Editor
{
    private AppearanceVariantsGenerator tgt;

    private void OnEnable()
    {
        tgt = target as AppearanceVariantsGenerator;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Process Combinations"))
        {
            Debug.Log("Generating variants");
            //tgt.GenerateVariants();
        }
    }
}