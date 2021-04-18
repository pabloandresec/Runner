using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxTeleport : MonoBehaviour
{
    [SerializeField] private float animTime = 0.5f;
    [SerializeField] private Transform destiny;
    [SerializeField] private Transform tpPoint;
    [SerializeField] private Vector2 teleportExitVel = Vector2.zero;
    [SerializeField] private CinemachineVirtualCamera cam;
    [Header("Audio")]
    [SerializeField] private AudioController audioController;
    [SerializeField] private int onTriggerEnter = 4;
    [Header("Next level")]
    [SerializeField] private bool teleportToNextLevel = false;
    [SerializeField] private int nextLevelBuildIndex = 2;
    private bool warping = false;
    float ortGraphicSize = 5;
    private Vector3 ogOffset;
    private CinemachineFramingTransposer trans;

    private void Start()
    {
        trans = cam.GetCinemachineComponent<CinemachineFramingTransposer>();
        ogOffset = trans.m_TrackedObjectOffset;
        ortGraphicSize = cam.m_Lens.OrthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null && !warping)
        {
            warping = true;
            rb.GetComponent<Motor>().enabled = false;
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;

            audioController.PlaySFX(onTriggerEnter);

            LeanTween.move(rb.gameObject, tpPoint.position, animTime);//move to tpPoint tween
            LeanTween.value(cam.gameObject, v3 => trans.m_TrackedObjectOffset = v3, ogOffset, Vector3.zero, animTime * 0.9f); // offset tween
            LeanTween.value(cam.gameObject, f => cam.m_Lens.OrthographicSize = f, ortGraphicSize, 0.1f, animTime + animTime * 0.1f).setOnComplete(() => { // zoom in tween
                //Cuando se termine de hacer un zoom in a la caja
                if(teleportToNextLevel)
                {
                    SceneManager.LoadScene(nextLevelBuildIndex);
                }
                else
                {
                    Teleport(rb);
                    LeanTween.value(cam.gameObject, v3 => trans.m_TrackedObjectOffset = v3, Vector3.zero, ogOffset, animTime * 0.9f); // offset tween
                    LeanTween.value(cam.gameObject, f => cam.m_Lens.OrthographicSize = f, 0.1f, ortGraphicSize, animTime).setOnComplete(() => { // zoom out tween
                                                                                                                                                //Cuando se termine de hacer un zoom out a la caja
                        warping = false;
                    });
                }
            });
        }
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
