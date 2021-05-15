using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BoxTeleport : MonoBehaviour
{
    [SerializeField] private Transform destiny;
    [SerializeField] private Transform tpPoint;
    [SerializeField] private Vector2 teleportExitVel = Vector2.zero;
    [SerializeField] private CinemachineVirtualCamera cam;
    [Header("Times")]
    [SerializeField] private float hideTime = 0.5f;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private float moveAndLockToZoomInTGT = 0.5f;
    [SerializeField] private float zoomInTime = 0.5f;
    [SerializeField] private float revertCamOffsetAndZoomOutTGT = 0.5f;
    [Header("Audio")]
    [SerializeField] private AudioController audioController;
    [SerializeField] private int onTriggerEnter = 4;
    [Header("Next level")]
    [SerializeField] private bool teleportToNextLevel = false;
    [SerializeField] private int nextLevelBuildIndex = 2;
    [Header("Animation Stuff")]
    [SerializeField] private Transform zoomInPoint;
    [SerializeField] private Transform zoomOutPoint;
    private bool warping = false;
    private Rigidbody2D tgt;
    float ortGraphicSize = 5;
    private Vector3 ogOffset;
    private CinemachineFramingTransposer trans;
    [SerializeField] private UnityEvent OnBoxEnter;


    private void Start()
    {
        trans = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        ogOffset = trans.m_TrackedObjectOffset;
        ortGraphicSize = cam.m_Lens.OrthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!warping)
            {
                tgt = collision.GetComponent<Rigidbody2D>();
                Debug.Log("Box Teleporting start!");
                OnBoxEnter?.Invoke();
                warping = true;
                tgt.GetComponent<Motor>().enabled = false;
                tgt.isKinematic = true;
                tgt.velocity = Vector2.zero;

                audioController.PlaySFX(onTriggerEnter);

                LeanTween.move(tgt.gameObject, tpPoint.position, hideTime).setOnComplete(() =>
                {
                    tgt.gameObject.SetActive(false);
                    StartCoroutine(WaitAndExecute(restTime, MoveAndZoomToTGT));
                });//move player to tpPoint tween
                if (teleportToNextLevel)
                {
                    GameObject.FindGameObjectWithTag("UI").GetComponent<MenuController>().LoadScene(nextLevelBuildIndex);
                }
            }
        }
    }

    private IEnumerator WaitAndExecute(float waitTime, Action v)
    {
        yield return new WaitForSeconds(waitTime);
        v?.Invoke();
    }

    private void MoveAndZoomToTGT()
    {
        LeanTween.move(tgt.gameObject, zoomInPoint.position, moveAndLockToZoomInTGT); // move disabled  player to zoom point
        LeanTween.value(cam.gameObject, v3 => trans.m_TrackedObjectOffset = v3, ogOffset, Vector3.zero, moveAndLockToZoomInTGT);//Offset tween
        LeanTween.value(cam.gameObject, f => cam.m_Lens.OrthographicSize = f, ortGraphicSize, 0.01f, moveAndLockToZoomInTGT).setOnComplete(() =>
        { // zoom in tween
          //Cuando se termine de hacer un zoom in a la caja
            if (teleportToNextLevel)
            {
                //GameObject.FindGameObjectWithTag("UI").GetComponent<MenuController>().LoadScene(nextLevelBuildIndex);
                //SceneManager.LoadScene(nextLevelBuildIndex);
            }
            else
            {
                Teleport(tgt);
                LeanTween.move(tgt.gameObject, zoomOutPoint.position, revertCamOffsetAndZoomOutTGT);
                LeanTween.value(cam.gameObject, v3 => trans.m_TrackedObjectOffset = v3, Vector3.zero, ogOffset, revertCamOffsetAndZoomOutTGT); // offset tween
                LeanTween.value(cam.gameObject, f => cam.m_Lens.OrthographicSize = f, 0.05f, ortGraphicSize, revertCamOffsetAndZoomOutTGT).setOnComplete(() => // zoom out tween
                {
                    //Cuando se termine de hacer un zoom out a la caja
                    StartCoroutine(WaitAndExecute(restTime, () =>
                    {
                        tgt.gameObject.SetActive(true);
                        warping = false;
                    }));
                });
            }
        });
    }

    private void Teleport(Rigidbody2D rb)
    {
        Vector3 delta = destiny.transform.position - rb.transform.position;

        cam.OnTargetObjectWarped(rb.transform, delta);
        rb.transform.position = destiny.transform.position;

        rb.isKinematic = false;
        rb.velocity = teleportExitVel;
        rb.GetComponent<Motor>().enabled = true;
    }
}
