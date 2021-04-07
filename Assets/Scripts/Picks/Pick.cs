using UnityEngine;
using System.Collections;
using System;

public class Pick : MonoBehaviour
{
    [SerializeField] private AppearanceWrapper appearance;
    [Header("References")]
    [SerializeField] private Animator[] anim;
    [Header("Sound index")]
    [SerializeField] private int onPickSoundIndex = 2;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterAppearanceHandler>().SwapAppearance(appearance);
            GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>().PlaySFX(onPickSoundIndex);
            Destroy(gameObject);
        }
    }

    private Transform GetRendererParentTransform()
    {
        Transform t = transform.Find("Renderer");
        if (t == null)
        {
            GameObject render = new GameObject("Renderer");
            render.transform.SetParent(this.transform);
            render.transform.localPosition = Vector3.zero;
        }
        return t;
    }

    private static void ClearChilds(Transform t)
    {
        if (t.childCount > 0)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                Destroy(t.GetChild(i).gameObject);
            }
        }
    }
}
