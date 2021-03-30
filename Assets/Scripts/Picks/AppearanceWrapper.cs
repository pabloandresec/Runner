using UnityEngine;
using System.Collections;
using System;

[Serializable]
public struct AppearanceWrapper
{
    [SerializeField] private int layerIndex;
    [SerializeField] private RuntimeAnimatorController appearanceController;

    public int LayerIndex { get => layerIndex; }
    public RuntimeAnimatorController AppearanceController { get => appearanceController; }
}
