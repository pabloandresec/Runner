using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnScreenCustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private InputController controller;

    bool pressed = false;

    private void Update()
    {
        if(pressed)
        {
            controller.Slide();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}
