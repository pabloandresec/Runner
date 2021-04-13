using UnityEngine;

public class PickAppearance : Pick
{
    [Header("Appearance")]
    [SerializeField] private AppearanceWrapper appearance;

    protected override void PickedUp(Collider2D collision)
    {
        base.PickedUp(collision);
        collision.GetComponent<CharacterAppearanceHandler>().SwapAppearance(appearance);
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>().PlaySFX(onPickSoundIndex);
        Destroy(gameObject);
    }
}
