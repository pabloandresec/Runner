using UnityEngine;
using UnityEditor;

public class ExtendedEditor : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty currentProperty;

    protected void DrawProperties(SerializedProperty property, bool drawChildren)
    {
        string lastPropPath = string.Empty;
        foreach (SerializedProperty p in property)
        {
            if (!string.IsNullOrEmpty(lastPropPath) && p.propertyPath.Contains(lastPropPath))
            {
                continue;
            }
            lastPropPath = p.propertyPath;
            EditorGUILayout.PropertyField(p, drawChildren);
            /*
            if(p.isArray && p.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUILayout.BeginHorizontal();
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                EditorGUILayout.EndHorizontal();
                if(p.isExpanded)
                {
                    EditorGUI.indentLevel += 1;
                    DrawProperties(p, drawChildren);
                    EditorGUI.indentLevel -= 1;
                }
            }
            else
            {
                if(!string.IsNullOrEmpty(lastPropPath) && p.propertyPath.Contains(lastPropPath))
                {
                    continue;
                }
                lastPropPath = p.propertyPath;
                EditorGUILayout.PropertyField(p, drawChildren);
            }
            */
        }

    }
}