using UnityEngine;
using System.Collections;
using System;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private Motor motor;
    [SerializeField] private GameUI uiController;
    [SerializeField] float jumpCoolOff = 0.25f;
    private MobileInput inputs;
    private Vector2 input = Vector2.zero;
    private bool jump = false;
    private bool canJump = true;
    private bool slide = false;
    private bool pressingSlidingButton = false;
    private float tPassed = 0;

    public void Slide()
    {
        slide = true;
    }

    private void Awake()
    {
        inputs = new MobileInput();
        inputs.Game.Slide.performed += ctx => Slide(ctx);
        inputs.Game.Slide.canceled += ctx => Slide(ctx);
        inputs.Game.Jump.performed += ctx => Jump(ctx);
        inputs.Game.ShowHelp.performed += ctx => ShowHelpMenu(ctx);
        inputs.Game.PauseGame.performed += ctx => ShowPauseMenu(ctx);
    }

    private void OnEnable()
    {
        inputs.Game.Enable();
    }

    private void OnDisable()
    {
        inputs.Game.Disable();
    }

    private void Slide(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            pressingSlidingButton = true;
        }
        else
        {
            pressingSlidingButton = false;
        }
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        jump = true;
    }

    private void ShowHelpMenu(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            //Debug.Log("Pressing help button");
            if (uiController.GameOnHelp)
            {
                uiController.UnPauseGame();
            }
            else
            {
                uiController.ShowHelp();
            }
        }
    }

    private void ShowPauseMenu(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            //Debug.Log("Pressing pause button");
            if (uiController.GamePaused)
            {
                uiController.UnPauseGame();
            }
            else
            {
                uiController.PauseGame();
            }
        }
    }

    private void Start()
    {
        if (motor == null) GetComponent<Motor>();
    }

    private void Update()
    {
        slide = pressingSlidingButton ? true : false;

        if (!canJump)
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
