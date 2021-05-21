using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomParticleEmitter : MonoBehaviour
{
    [SerializeField] private GameObject[] objs;
    [SerializeField] private float arcDiameter = 2f;
    [SerializeField] private float defaultVel = 2f;
    GameObject[] spawnedObjects;
    [Range(0,360)]
    [SerializeField] private float startTime = 0f;
    [SerializeField] private float offsetFromStartT = 45f;
    private float theta;
    private float tPassed = 0;
    private float life = 5f;
    private bool active = false;


    private void Start()
    {
        CalculateTheta();
    }

    private void Update()
    {/*
        if(Input.GetKeyDown(KeyCode.P))
        {
            Emit();
        }
        */
        if(active)
        {
            tPassed += Time.deltaTime;
            if(tPassed > life)
            {
                tPassed = 0;
                active = false;
                DisableParticles();
            }
        }
    }

    private void DisableParticles()
    {
        foreach (GameObject game in objs)
        {
            game.SetActive(false);
        }
    }

    private void OnValidate()
    {
        if(offsetFromStartT <= startTime)
        {
            offsetFromStartT = startTime;
        }
        CalculateTheta();
    }

    private void CalculateTheta()
    {
        theta = (Mathf.PI * arcDiameter - (offsetFromStartT - startTime)) / objs.Length;
    }

    public void Emit()
    {
        active = true;

        spawnedObjects = new GameObject[objs.Length];

        foreach (GameObject game in objs)
        {
            game.SetActive(true);
            game.transform.position = transform.position;
            game.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        for (int i = 0; i < objs.Length; i++)
        {
            float x = defaultVel * Mathf.Cos(theta * i);
            float y = defaultVel * Mathf.Sin(theta * i);
            objs[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y));
        }
    }

    public void Emit(float delay)
    {
        StartCoroutine(WaitAndExecute(delay, Emit));
    }

    private IEnumerator WaitAndExecute(float delay, Action emit)
    {
        yield return new WaitForSeconds(delay);
        emit?.Invoke();
    }
}
