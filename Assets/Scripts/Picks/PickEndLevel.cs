using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickEndLevel : Pick
{
    [Header("Next level")]
    [Min(0)]
    [SerializeField] private int nextLevel = 2;

    protected override void PickedUp(Collider2D collision)
    {
        base.PickedUp(collision);
        SceneManager.LoadSceneAsync(nextLevel);
    }
}
