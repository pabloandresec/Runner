using System;
using UnityEngine;
using UnityEngine.Events;

public class PickAppearance : Pick
{
    [Header("Appearance")]
    [SerializeField] private AppearanceWrapper appearance;
    [SerializeField] private UnityEvent[] events;
    [SerializeField] private bool lockAndDisableHairs = false;

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
        if(lockAndDisableHairs && appearance.LayerIndex == 0)
        {
            collision.GetComponent<CharacterAppearanceHandler>().LockHairPicks();
        }
        else if(!lockAndDisableHairs && appearance.LayerIndex == 0)
        {
            collision.GetComponent<CharacterAppearanceHandler>().UnlockHairPicks();
        }
        
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
