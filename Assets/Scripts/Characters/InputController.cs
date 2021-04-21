using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
    [SerializeField] private Motor motor;

    private Vector2 input = Vector2.zero;
    private bool jump = false;
    private bool slide = false;

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
    }

    private void FixedUpdate()
    {
        motor.MoveLeft(slide);
        motor.Jump(jump);
        slide = false;
        jump = false;
    }
}
