﻿using System;
using UnityEngine;

public class CharacterAppearanceHandler : MonoBehaviour
{
    [SerializeField] private Animator[] layers;

    public void SwapAppearance(AppearanceWrapper appearance)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if(i == appearance.LayerIndex)
            {
                layers[i].runtimeAnimatorController = appearance.AppearanceController;
                layers[i].gameObject.SetActive(true);
                SyncAnimators();
                return;
            }
        }
    }

    public void ChangeLayerColor(int layer, Color color)
    {
        if (layer < 0 || layer >= layers.Length) return;
        layers[layer].GetComponent<SpriteRenderer>().color = color;
        SyncAnimators();
    }

    private void SyncAnimators()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if(layers[i].gameObject.activeInHierarchy)
            {
                layers[i].gameObject.SetActive(false);
                layers[i].gameObject.SetActive(true);
            }
        }
    }
}