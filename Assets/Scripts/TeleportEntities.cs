using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEntities : MonoBehaviour
{
    [SerializeField] private Transform destiny;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector3 diff = rb.transform.position - transform.position;
            rb.transform.position = destiny.position + diff;
        }
    }
}
