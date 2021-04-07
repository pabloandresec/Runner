using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TeleportEntities : MonoBehaviour
{
    [SerializeField] private Transform destiny;
    [SerializeField] private CinemachineVirtualCamera cam;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector3 posPlayerDiff = rb.transform.position - transform.position;
            Vector3 finalPos = destiny.position + posPlayerDiff;
            Vector3 delta = finalPos - cam.transform.position + (cam.transform.position - rb.transform.position);
            cam.OnTargetObjectWarped(rb.transform, delta);
            rb.transform.position = finalPos;
        }
    }
}
