using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class
/// </summary>
public class Motor : MonoBehaviour
{
    [SerializeField] private Collider2D col = null;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private Collider2D top = null;
    [Header("Sensors")]
    [SerializeField] private Transform[] sensors = null;
    [Header("Motion Settings")]
    [SerializeField] private MotionMode motionMode = MotionMode.ACCELERATION;
    [SerializeField] private float acceleration = 5;
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float jumpForce = 6;
    [Tooltip("Solo valido en motion INSTANTANEOUS")]
    [SerializeField] private float slideSpeed = 5;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundRadius = 0.2f;
    private Vector2 currentMotion = Vector2.zero;
    private bool isGrounded = false;
    private bool topWallSensor = false;
    private bool bottomWallSensor = false;
    private bool getUpLocked = false;
    private bool slideState = false;

    public float CurrentXVel { get => rb.velocity.x; }
    public float CurrentYVel { get => rb.velocity.y; }
    public bool SlideState { get => slideState; }
    public bool IsGrounded { get => isGrounded; }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(sensors[0].position, groundRadius, groundMask);
        topWallSensor = Physics2D.OverlapCircle(sensors[2].position, groundRadius, groundMask);
        bottomWallSensor = Physics2D.OverlapCircle(sensors[1].position, groundRadius, groundMask);
        getUpLocked = Physics2D.OverlapCircle(sensors[3].position, groundRadius, groundMask);
    }

    public void MoveLeft(bool slide)
    {
        switch (motionMode)
        {
            case MotionMode.ACCELERATION:
                MoveAcceleration(slide);
                break;
            case MotionMode.INSTANTANEOUS:
                MoveInstantaneous(slide);
                break;
        }
    }

    private void MoveAcceleration(bool slide)
    {
        if(IsGrounded)
        {
            if (getUpLocked)
            {
                slide = true;
            }
            if (slide)
            {
                currentMotion += new Vector2(-1, 0) * acceleration * Time.fixedDeltaTime;
                currentMotion = new Vector2(Mathf.Clamp(currentMotion.x, 0, maxSpeed), currentMotion.y);
            }
            else if (!bottomWallSensor)
            {
                currentMotion += new Vector2(1, 0) * acceleration * Time.fixedDeltaTime;
                currentMotion = Vector2.ClampMagnitude(currentMotion, maxSpeed);
            }
            else
            {
                currentMotion = new Vector2(0, 0);
            }
            DisableTopCollider(slide);
            slideState = slide;
            rb.velocity = currentMotion;
        }
    }

    private void MoveInstantaneous(bool slide)
    {
        if (!bottomWallSensor) // Si no hay una pared delante del sensor inferior
        {
            if (getUpLocked)
            {
                slide = true;
            }
            float s = slide ? slideSpeed : maxSpeed;
            slideState = slide ? true : false;
            DisableTopCollider(slide);

            rb.velocity = new Vector2(s, rb.velocity.y);
        }
        else
        {
            slideState = false;
        }
    }

    private void DisableTopCollider(bool slide)
    {
        if (top != null && slide)
        {
            top.enabled = false;
        }
        else
        {
            top.enabled = true;
        }
    }

    public void Jump(bool jump)
    {
        if(isGrounded && jump && !slideState)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.red : Color.white;
        Gizmos.DrawWireSphere(sensors[0].position, groundRadius);
        Gizmos.color = bottomWallSensor ? Color.green : Color.white;
        Gizmos.DrawWireSphere(sensors[1].position, groundRadius);
        Gizmos.color = topWallSensor ? Color.red : Color.white;
        Gizmos.DrawWireSphere(sensors[2].position, groundRadius);
        Gizmos.color = getUpLocked ? Color.red : Color.white;
        Gizmos.DrawWireSphere(sensors[3].position, groundRadius);
    }
}
public enum MotionMode
{
    ACCELERATION,
    INSTANTANEOUS
}
