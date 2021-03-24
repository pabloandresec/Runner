using UnityEngine;
using System.Collections;
using System;

public class Pick : MonoBehaviour
{
    [SerializeField] private ItemData data;
    [Header("References")]
    [SerializeField] private Animator[] anim;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void RefreshVisualData()
    {
        if(data != null)
        {
            return;
        }
        if(data.IconAnimControllers.Length > 0)
        {
            Transform t = GetRendererParentTransform();
            CreateRendererAnimations(t);
        }
    }

    private void CreateRendererAnimations(Transform t)
    {
        ClearChilds(t);
        anim = new Animator[data.IconAnimControllers.Length];

        for (int i = 0; i < data.IconAnimControllers.Length; i++)
        {
            GameObject currentLayer = new GameObject(data.Id + "_Layer_" + i);
            currentLayer.transform.SetParent(t);
            currentLayer.transform.localPosition = Vector3.zero;
            SpriteRenderer csr = currentLayer.AddComponent<SpriteRenderer>();
            Animator cAnim = currentLayer.AddComponent<Animator>();
            cAnim.runtimeAnimatorController = data.IconAnimControllers[i];
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
