using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class TeleportEntities : MonoBehaviour
{
    [SerializeField] private Transform destiny;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private float restingTime = 4;
    [SerializeField] private UnityEvent onTeleportEvent;
    bool warped = false;
    float tPassed = 0;

    Vector3 posPlayerDiff = Vector3.zero;
    Vector3 finalPos = Vector3.zero;
    Vector3 playerCamDifference = Vector3.zero;
    Vector3 delta = Vector3.zero;

    private void FixedUpdate()
    {
        if(warped)
        {
            tPassed += Time.fixedDeltaTime;
            if(tPassed > restingTime)
            {
                tPassed = 0;
                warped = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OldTeleport(collision);
    }

    private void OldTeleport(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null && !warped)
        {
            onTeleportEvent?.Invoke();
            posPlayerDiff = rb.transform.position - transform.position;
            finalPos = destiny.position + posPlayerDiff;
            playerCamDifference = cam.transform.position - rb.transform.position;
            delta = finalPos - cam.transform.position + playerCamDifference;
            cam.OnTargetObjectWarped(rb.transform, delta);
            rb.transform.position = finalPos;
            Debug.Log("TELEPORTED!");
            warped = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Debug.DrawLine(transform.position, destiny.position);
    }
}
