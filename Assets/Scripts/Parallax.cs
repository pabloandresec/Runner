using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Rigidbody2D player;
    [Range(0f,2f)]
    [SerializeField] private float rate = 1;
    [SerializeField] private float currentSpeed = 0;
    [SerializeField] private float boundX;
    [SerializeField] private float absX;

    void Start()
    {
        boundX = transform.GetChild(1).GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = -player.velocity.normalized.x * rate * Time.deltaTime;
        transform.position += new Vector3(currentSpeed , 0, 0);
        absX = Mathf.Abs(transform.localPosition.x);
        if (absX >= boundX)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
