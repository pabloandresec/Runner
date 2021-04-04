using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    private float length, startXPos, startYPos;
    [SerializeField] private ParalaxMode mode = ParalaxMode.CAM;
    public Rigidbody2D player;
    public GameObject cam;
    [Range(0f,1f)]
    public float parallaxEffect;
    public float rbAcc = 0;

    [SerializeField] private float acc;
    [SerializeField] private float xMotion;
    [SerializeField] private float xDistFromStart;
    [SerializeField] private float xOffset = 0;

    public float Acc { get => acc; set => acc = value; }
    public float XDist { get => xMotion; set => xMotion = value; }
    public float XDistFromStart { get => xDistFromStart; set => xDistFromStart = value; }
    public float XOffset { get => xOffset; set => xOffset = value; }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        startXPos = transform.position.x;
        //startYPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case ParalaxMode.CAM:
                CamModeParalax();
                break;
            case ParalaxMode.PLAYER:
                PlayerModeParalax();
                break;
        }
        
    }

    private void PlayerModeParalax()
    {
        rbAcc += player.velocity.x;
        acc = (cam.transform.position.x * (1 - parallaxEffect));
        xMotion = (player.velocity.x * parallaxEffect);
        //float yDist = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startXPos + xMotion + xOffset, transform.position.y, transform.position.z);
        if (acc > startXPos + length)
        {
            startXPos += length;
        }
        else if (acc < startXPos - length)
        {
            startXPos -= length;
        }
    }

    private void CamModeParalax()
    {
        acc = (cam.transform.position.x * (1 - parallaxEffect));
        xMotion = (cam.transform.position.x * parallaxEffect);
        xDistFromStart = startXPos + xMotion + xOffset;

        transform.position = new Vector3(xDistFromStart, transform.position.y, transform.position.z);
        if (acc > startXPos + length)
        {
            startXPos += length;
        }
        else if (acc < startXPos - length)
        {
            startXPos -= length;
        }
    }
}

public enum ParalaxMode
{
    CAM,
    PLAYER
}