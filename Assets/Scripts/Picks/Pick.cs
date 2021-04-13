using UnityEngine;
using System.Collections;
using System;

public abstract class Pick : MonoBehaviour
{
    [Header("Tweening")]
    [SerializeField] protected bool tween = false;
    [SerializeField] protected float tweenTime = 1f;
    [SerializeField] protected TweenType tweenType = TweenType.RotPingPong;
    [Header("Sound index")]
    [SerializeField] protected int onPickSoundIndex = 2;


    private void Start()
    {
        switch (tweenType)
        {
            case TweenType.RotPingPong:
                LeanTween.rotate(gameObject, new Vector3(0, 0, 40f), 1f).setEaseInOutExpo().setLoopPingPong();
                break;
            case TweenType.ScalePingPong:
                LeanTween.scale(gameObject,new Vector3(0.5f, 1.5f, 1f), 1f).setEaseInOutExpo().setLoopPingPong();
                break;
            case TweenType.Both:
                LeanTween.rotate(gameObject, new Vector3(0, 0, 40f), 1f).setEaseInOutExpo().setLoopPingPong();
                LeanTween.scale(gameObject, new Vector3(0.5f, 1.5f, 1f), 1f).setEaseInOutExpo().setLoopPingPong();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PickedUp(collision);
        }
    }

    protected virtual void PickedUp(Collider2D collision)
    {
        
    }
}

public enum TweenType
{
    RotPingPong,
    ScalePingPong,
    Both
}