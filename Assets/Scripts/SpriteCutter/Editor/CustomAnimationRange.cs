using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public struct CustomAnimationRange
{
    public string name;
    public int from;
    public int to;
    public int fps;
    public Vector2 pivot;
    public bool loop;
}
