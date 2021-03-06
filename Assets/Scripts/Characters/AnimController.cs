using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimController : MonoBehaviour
{
    [SerializeField] protected Animator[] anims;
    [SerializeField] protected Motor motor;
    [SerializeField] protected InputController ic;
}
