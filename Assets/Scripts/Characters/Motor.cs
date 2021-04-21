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
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float[] sensorRadios = null;
    [Header("Motion Settings")]
    [SerializeField] private MotionMode motionMode = MotionMode.ACCELERATION;
    [SerializeField] private float acceleration = 5;
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float minSlideSpeed = 1;
    [SerializeField] private float jumpForce = 6;
    [Tooltip("Solo valido en motion INSTANTANEOUS")]
    [SerializeField] private float slideSpeed = 5;
    
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

    public bool colliding = false;
    public Vector2 direction = Vector2.zero;
    public Vector2 auxDir = Vector2.zero;
    public Vector2 rbVelocity = Vector2.zero;

    [Header("Sound index table")]
    [Min(0)]
    [SerializeField] private int onSlide = 0;
    [SerializeField] private int onJump = 0;
    [SerializeField] private int onLand = 0;

    private AudioController audio;

    private void Start()
    {
        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }

    private void Update()
    {
        rbVelocity = rb.velocity;
        isGrounded = Physics2D.OverlapCircle(sensors[0].position, sensorRadios[0], groundMask);
        topWallSensor = Physics2D.OverlapCircle(sensors[2].position, sensorRadios[2], groundMask);
        bottomWallSensor = Physics2D.OverlapCircle(sensors[1].position, sensorRadios[1], groundMask);
        getUpLocked = Physics2D.OverlapCircle(sensors[3].position, sensorRadios[3], groundMask);
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
            case MotionMode.EXPERIMENTAL:
                MoveAccelerationExp(slide);
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
                audio.StartSFXPlayLooped(onSlide);
                currentMotion += -Vector2.right * acceleration * Time.fixedDeltaTime;
                currentMotion = new Vector2(Mathf.Clamp(currentMotion.x, minSlideSpeed, maxSpeed), currentMotion.y);
            }
            else if (!bottomWallSensor)
            {
                currentMotion += Vector2.right * acceleration * Time.fixedDeltaTime;
                currentMotion = Vector2.ClampMagnitude(currentMotion, maxSpeed);
            }
            else
            {
                currentMotion = new Vector2(0, 0);
            }
            if(!slide)
            {
                audio.StopSFXPlayLooped(onSlide);
            }

            DisableTopCollider(slide);
            slideState = slide;
            rb.velocity = currentMotion;
        }
        else
        {
            slideState = false;
            DisableTopCollider(slideState);
            audio.StopSFXPlayLooped(onSlide);

            if (!bottomWallSensor && !topWallSensor && rb.velocity.x < maxSpeed * 0.3f)
            {
                rb.AddForce(new Vector2(1, 1) * jumpForce);
            }
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -15, 20));
            currentMotion.x = rb.velocity.x;
        }
    }

    private void MoveAccelerationExp(bool slide)
    {
        if (IsGrounded && colliding)
        {
            if (getUpLocked)
            {
                slide = true;
            }
            if (slide)
            {
                currentMotion += -direction * acceleration * Time.fixedDeltaTime;
                currentMotion = new Vector2(Mathf.Clamp(currentMotion.x, minSlideSpeed, maxSpeed), currentMotion.y);
            }
            else if (!bottomWallSensor)
            {
                currentMotion += direction * acceleration * Time.fixedDeltaTime;
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
        else
        {
            if (!bottomWallSensor && !topWallSensor && rb.velocity.x < maxSpeed * 0.3f)
            {
                rb.AddForce(new Vector2(1, 1) * jumpForce);
            }
            slideState = false;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -15, 20));
            currentMotion.x = rb.velocity.x;
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
        if(isGrounded && jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audio.PlaySFX(onJump);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isGrounded)
        {
            //audio.PlaySFX(onLand);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        colliding = true;

        if (isGrounded)
        {
            direction = -Vector2.Perpendicular(collision.GetContact(0).normal).normalized;
            if(direction.y < 0)
            {
                direction.y = 0;
            }
        }
        else
        {
            direction = Vector2.zero;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        colliding = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.red : Color.white;
        Gizmos.DrawWireSphere(sensors[0].position, sensorRadios[0]);
        Gizmos.color = bottomWallSensor ? Color.green : Color.white;
        Gizmos.DrawWireSphere(sensors[1].position, sensorRadios[1]);
        Gizmos.color = topWallSensor ? Color.red : Color.white;
        Gizmos.DrawWireSphere(sensors[2].position, sensorRadios[2]);
        Gizmos.color = getUpLocked ? Color.red : Color.white;
        Gizmos.DrawWireSphere(sensors[3].position, sensorRadios[3]);
    }
}
public enum MotionMode
{
    ACCELERATION,
    INSTANTANEOUS,
    EXPERIMENTAL
}
