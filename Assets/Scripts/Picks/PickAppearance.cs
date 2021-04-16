using System;
using UnityEngine;
using UnityEngine.Events;

public class PickAppearance : Pick
{
    [Header("Appearance")]
    [SerializeField] private AppearanceWrapper appearance;
    [SerializeField] private UnityEvent[] events;

    protected override void PickedUp(Collider2D collision)
    {
        base.PickedUp(collision);
        collision.GetComponent<CharacterAppearanceHandler>().SwapAppearance(appearance);
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>().PlaySFX(onPickSoundIndex);
        AppearanceHasUpdated();
        if(events.Length > 0)
        {
            for (int i = 0; i < events.Length; i++)
            {
                events[i].Invoke();
            }
        }
        
        Destroy(gameObject);
    }
}
