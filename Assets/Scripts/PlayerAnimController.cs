using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : AnimController
{
    private void Update()
    {
        anim.SetFloat("xVel", motor.CurrentXVel);
        anim.SetBool("isGrounded", !motor.IsGrounded);
        anim.SetBool("slide", motor.SlideState);
        anim.SetFloat("yVel", motor.CurrentYVel);
    }
}
