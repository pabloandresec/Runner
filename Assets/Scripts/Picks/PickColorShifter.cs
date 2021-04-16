using UnityEngine;
using System.Collections;

public class PickColorShifter : Pick
{
    [Header("Color")]
    [SerializeField] private Color color;
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private int layer;


    private void OnValidate()
    {
        icon.color = color;
    }

    protected override void PickedUp(Collider2D collision)
    {
        base.PickedUp(collision);
        collision.GetComponent<CharacterAppearanceHandler>().ChangeLayerColor(layer, color);
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>().PlaySFX(onPickSoundIndex);
        AppearanceHasUpdated();
        Destroy(gameObject);
    }
}
