using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemData" , menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private RuntimeAnimatorController[] iconAnimControllers;

    public string Id { get => id; }
    public RuntimeAnimatorController[] IconAnimControllers { get => iconAnimControllers; }
}