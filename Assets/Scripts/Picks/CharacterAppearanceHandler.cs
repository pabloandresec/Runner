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
                SyncAnimators();
                return;
            }
        }
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