using UnityEngine;
using System.Collections;
using System;

[Serializable]
public struct AppearanceWrapper
{
    [SerializeField] private int layerIndex;
    [SerializeField] private RuntimeAnimatorController appearanceController;
    [SerializeField] private Color color;

    public AppearanceWrapper(int layerIndex, RuntimeAnimatorController appearanceController, Color color)
    {
        this.layerIndex = layerIndex;
        this.appearanceController = appearanceController;
        this.color = color;
    }

    public int LayerIndex { get => layerIndex; }
    public RuntimeAnimatorController AppearanceController { get => appearanceController; }
    public Color Color { get => color; }
}
