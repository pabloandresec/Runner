using System;
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
                SpriteRenderer sr = layers[i].GetComponent<SpriteRenderer>();
                if(appearance.Color.a != 0)
                {
                    sr.color = appearance.Color;
                }
                SyncAnimators();
                return;
            }
        }
    }

    public void ClearLayer(int index)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (i == index)
            {
                layers[i].runtimeAnimatorController = null;
                layers[i].gameObject.SetActive(false);
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