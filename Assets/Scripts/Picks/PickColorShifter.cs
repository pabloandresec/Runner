using UnityEngine;
using System.Collections;

public class PickColorShifter : Pick
{
    [Header("Color")]
    [SerializeField] private Color color;
    [SerializeField] private string colorName;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private int layer;


    private void OnValidate()
    {
        icon.color = color;
    }

    protected override void PickedUp(Collider2D collision)
    {
        base.PickedUp(collision);
        collision.GetComponent<CharacterAppearanceHandler>().ChangeLayerColor(layer, color, colorName);
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>().PlaySFX(onPickSoundIndex);
        AppearanceHasUpdated();
        collision.GetComponent<CharacterAppearanceHandler>().ChangeLayerColorName(colorName, 1);
        Destroy(gameObject);
    }
}
