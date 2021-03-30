using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : AnimController
{
    private void Update()
    {
        for (int i = 0; i < anims.Length; i++)
        {
            if (anims[i] == null || !anims[i].gameObject.activeInHierarchy) return;
            anims[i].SetFloat("xVel", motor.CurrentXVel);
            anims[i].SetBool("isGrounded", !motor.IsGrounded);
            anims[i].SetBool("slide", motor.SlideState);
            anims[i].SetFloat("yVel", motor.CurrentYVel);
        }
    }
}
