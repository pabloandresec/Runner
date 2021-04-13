using UnityEngine;
using System.Collections;

public class PickColorShifter : Pick
{
    [Header("Color")]
    [SerializeField] private Color color;
    [SerializeField] private int layer;

    protected override void PickedUp(Collider2D collision)
    {
        base.PickedUp(collision);
        collision.GetComponent<CharacterAppearanceHandler>().ChangeLayerColor(layer, color);
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>().PlaySFX(onPickSoundIndex);
        Destroy(gameObject);
    }
}
