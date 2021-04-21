using UnityEngine;
using System.Collections;
using System;

[Serializable]
public struct AppearanceWrapper
{
    [SerializeField] private int layerIndex;
    [SerializeField] private RuntimeAnimatorController appearanceController;
    [SerializeField] private Color color;
    [SerializeField] private string colorName;

    public AppearanceWrapper(int layerIndex, RuntimeAnimatorController appearanceController, Color color, string colorName)
    {
        this.layerIndex = layerIndex;
        this.appearanceController = appearanceController;
        this.color = color;
        this.colorName = colorName;
    }

    public int LayerIndex { get => layerIndex; }
    public RuntimeAnimatorController AppearanceController { get => appearanceController; }
    public Color Color { get => color; }
    public string ColorName { get => colorName; set => colorName = value; }
}
