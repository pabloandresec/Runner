using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickProfession : Pick
{
    [Header("Profession settings")]

    [SerializeField] private string id;
    public event Action<string, PickProfession> onPickedItem;

    protected override void PickedUp(Collider2D collision)
    {
        base.PickedUp(collision);
        onPickedItem?.Invoke(id, this);
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>().PlaySFX(onPickSoundIndex);
        Destroy(gameObject);
    }
}
