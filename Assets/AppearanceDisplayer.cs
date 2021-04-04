using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearanceDisplayer : MonoBehaviour
{
    [SerializeField] private Image[] layers;

    public void SetSprite(int layerIndex, Sprite s)
    {
        layers[layerIndex].sprite = s;
    }
}
