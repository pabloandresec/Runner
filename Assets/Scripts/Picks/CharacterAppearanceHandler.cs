using System;
using UnityEngine;

public class CharacterAppearanceHandler : MonoBehaviour
{
    [SerializeField] private PersistentData data;
    [SerializeField] private Animator[] layers;

    private bool lockedHairs = false;

    public bool LockedHairs { get => lockedHairs; }

    public void SwapAppearance(AppearanceWrapper appearance)
    {
        if(lockedHairs && appearance.LayerIndex == 1)
        {
            Debug.Log("Hair locked!");
            return;
        }

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

    public void LockHairPicks()
    {
        lockedHairs = true;
        layers[1].runtimeAnimatorController = null;
        layers[1].gameObject.GetComponent<SpriteRenderer>().sprite = null;
        Debug.Log("Hair locked!");
    }

    public void UnlockHairPicks()
    {
        lockedHairs = false;
        Debug.Log("Hair unlocked!");
    }

    public void ChangeLayerColorName(string colorName, int layerIndex)
    {
        data.Layers[layerIndex].ColorName = colorName;
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

    public void ChangeLayerColor(int layer, Color color, string colorName)
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