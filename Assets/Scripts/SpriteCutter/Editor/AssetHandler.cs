using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenAssetInEditor(int instanceID, int line)
    {
        SpriteCutterWindowData data = EditorUtility.InstanceIDToObject(instanceID) as SpriteCutterWindowData;
        if(data != null)
        {
            SpriteCutterWindow.ShowWindow(data);
            return true;
        }
        return false;
    }
}
