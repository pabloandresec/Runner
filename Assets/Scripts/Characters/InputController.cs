using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    [SerializeField] private Motor motor;
    [SerializeField] float jumpCoolOff = 0.25f;
    private Vector2 input = Vector2.zero;
    private bool jump = false;
    private bool canJump = true;
    private bool slide = false;
    private float tPassed = 0;

    private void Start()
    {
        if (motor == null) GetComponent<Motor>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
        }
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            slide = true;
        }
        if(Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.position.y > Screen.height / 2)
            {
                if(canJump)
                {
                    jump = true;
                    canJump = false;
                }
            }
            else
            {
                slide = true;
            }
        }

        if(!canJump)
        {
            tPassed += Time.deltaTime;
            if(tPassed > jumpCoolOff)
            {
                tPassed = 0;
                canJump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        motor.MoveLeft(slide);
        motor.Jump(jump);
        slide = false;
        jump = false;
    }
}
