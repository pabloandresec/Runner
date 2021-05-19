﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] protected GameObject[] mainMenus;
    [Range(0,4)]
    [SerializeField] protected int fadeDirection = 0;
    [SerializeField] protected float fadeTime = 0.2f;
    private Dictionary<string, Vector2> menuPositions;
    protected bool swapingMenus;
    [SerializeField] protected Image blackOverlay;
    [SerializeField] protected bool unfadeOnStart = true;


    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        StoreMenuPositions();
        if (blackOverlay != null)
        {
            blackOverlay.gameObject.SetActive(true);
            blackOverlay.raycastTarget = true;
            blackOverlay.color = Color.black;
            if(unfadeOnStart)
            {
                UnfadeScreenOverlay();
            }
        }
    }

    public void UnfadeScreenOverlay()
    {
        CanvasGroup overlayCanvasGroup = blackOverlay.GetComponent<CanvasGroup>();
        //LeanTween.color(blackOverlay.gameObject, Color.clear, 1f).setOnComplete(() => { blackOverlay.raycastTarget = false; });
        LeanTween.value(blackOverlay.gameObject, (nc) => { overlayCanvasGroup.alpha = nc; }, 1f, 0f, 1f).setOnComplete(() => {
            blackOverlay.raycastTarget = false;
            overlayCanvasGroup.gameObject.SetActive(false);
        });
    }

    protected void FadeScreenOverlay()
    {
        blackOverlay.gameObject.SetActive(true);
        blackOverlay.raycastTarget = true;
        CanvasGroup overlayCanvasGroup = blackOverlay.GetComponent<CanvasGroup>();
        //LeanTween.color(blackOverlay.gameObject, Color.black, 1f);
        //LeanTween.color(blackOverlay.gameObject, Color.clear, 1f).setOnComplete(() => { blackOverlay.raycastTarget = false; });
        LeanTween.value(blackOverlay.gameObject, (nc) => { overlayCanvasGroup.alpha = nc; }, 0f, 1f, 1f);
    }

    protected void StoreMenuPositions()
    {
        CanvasGroup[] canvases = Resources.FindObjectsOfTypeAll<CanvasGroup>();
        menuPositions = new Dictionary<string, Vector2>();
        for (int i = 0; i < canvases.Length; i++)
        {
            menuPositions.Add(canvases[i].transform.name, canvases[i].GetComponent<RectTransform>().anchoredPosition);
        }
        Debug.Log(canvases.Length + " menu positions saved for later use");
    }

    /// <summary>
    /// Cambia inmediatamente de menu
    /// </summary>
    /// <param name="i"></param>
    protected void SwitchMenu(int i)
    {
        if (i < 0 || i >= mainMenus.Length)
        {
            Debug.LogError("Out of index error in menus array");
        }
        foreach (GameObject m in mainMenus)
        {
            m.SetActive(false);
        }
        mainMenus[i].SetActive(true);
    }

    /// <summary>
    /// Cambia el alpha de algun grupo
    /// </summary>
    /// <param name="menuGroup"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    protected void SetAlpha(CanvasGroup menuGroup, float from, float to)
    {
        menuGroup.interactable = false;
        menuGroup.alpha = from;
        LeanTween.value(menuGroup.gameObject, (v) => { menuGroup.alpha = v; }, from, to, fadeTime).setOnComplete(() =>
        {
            if (menuGroup.alpha == 1)
            {
                menuGroup.interactable = true;
            }
        });
    }

    /// <summary>
    /// Setea la direccion en la que los menus iran mientras estan en fade
    /// </summary>
    /// <param name="direction"></param>
    public void SetFadeDirection(int direction)
    {
        fadeDirection = Mathf.Clamp(direction, 0, 5);
        Debug.Log("Direction");
    }

    /// <summary>
    /// De Acuredo a la direccion de "directionToFadeFrom" calcular el destino del fade
    /// </summary>
    /// <param name="rt"></param>
    /// <returns></returns>
    protected Vector2 GetFadeTargetPosition(RectTransform rt)
    {
        Vector2 t = Vector2.zero;
        switch (fadeDirection)
        {
            case 0: //North
                t += new Vector2(0, rt.rect.size.y / 2);
                break;
            case 1: //East
                t += new Vector2(rt.rect.size.x / 2, 0);
                break;
            case 2: //South
                t -= new Vector2(0, rt.rect.size.y / 2);
                break;
            case 3: //West
                t -= new Vector2(rt.rect.size.x / 2, 0);
                break;
        }

        return t;
    }

    public void LoadScene(int index)
    {
        if (blackOverlay != null)
        {
            StartCoroutine(AsyncLoadScene(index));
        }
        else
        {
            SceneManager.LoadScene(index);
        }
    }
    public void LoadScene(string sceneName)
    {
        if (blackOverlay != null)
        {
            StartCoroutine(AsyncLoadScene(sceneName));
        }
        else
        {
            SceneManager.LoadScene("Ending_" + sceneName);
        }
    }

    private IEnumerator AsyncLoadScene(string sceneName)
    {
        float tP = 0;
        AsyncOperation loadEndingOP = SceneManager.LoadSceneAsync("Ending_" + sceneName,LoadSceneMode.Single);
        bool doneFade = false;
        //LeanTween.color(blackOverlay.GetComponent<RectTransform>(), Color.black, 1f).setOnComplete(() => {
        //    Debug.Log("Fade completed");
        //    doneFade = true; });

        blackOverlay.gameObject.SetActive(true);
        blackOverlay.raycastTarget = true;
        CanvasGroup overlayCanvasGroup = blackOverlay.GetComponent<CanvasGroup>();
        LeanTween.value(blackOverlay.gameObject, (nc) => { overlayCanvasGroup.alpha = nc; }, 0f, 1f, 1f).setOnComplete(() => doneFade = true);

        while (!loadEndingOP.isDone && !doneFade)
        {
            tP += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Time Passed = " + tP);
        Debug.Log("Completed!");
    }
    private IEnumerator AsyncLoadScene(int sceneIndex)
    {
        float tP = 0;
        AsyncOperation loadEndingOP = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        bool doneFade = false;
        blackOverlay.gameObject.SetActive(true);
        blackOverlay.raycastTarget = true;
        CanvasGroup overlayCanvasGroup = blackOverlay.GetComponent<CanvasGroup>();
        LeanTween.value(blackOverlay.gameObject, (nc) => { overlayCanvasGroup.alpha = nc; }, 0f, 1f, 1f).setOnComplete(() => doneFade = true);

        while (!loadEndingOP.isDone && !doneFade)
        {
            tP += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Completed!");
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void TweenOverlayAlpha(float alpha)
    {
        Color newColor = new Color(1, 1, 1, alpha);
        LeanTween.color(blackOverlay.GetComponent<RectTransform>(), newColor, 0.5f);
    }

    public void TweenOverlayToBlack(float time)
    {
        Color newColor = Color.black;
        LeanTween.color(blackOverlay.GetComponent<RectTransform>(), newColor, 0.5f);
    }

    public void TweenOverlayToClear(float time)
    {
        Color newColor = Color.clear;
        LeanTween.color(blackOverlay.GetComponent<RectTransform>(), newColor, 0.5f);
    }

    #region FadeMenu
    public void FadeInMenu(GameObject menu)
    {
        swapingMenus = true;

        RectTransform menuRect = menu.GetComponent<RectTransform>();
        Vector2 intialPos = menuRect.anchoredPosition + GetFadeTargetPosition(menuRect);
        menuRect.anchoredPosition = intialPos;
        //Debug.Log("getting " + menu.transform.name + " og pos");
        Vector2 tgtPos = menuPositions[menu.transform.name];

        SetAlpha(menu.GetComponent<CanvasGroup>(), 0, 1);
        menu.SetActive(true);
        LeanTween.value(menu, (vec) =>
        {
            menuRect.anchoredPosition = vec;
        }, intialPos, tgtPos, fadeTime).setOnComplete(() =>
        {
            swapingMenus = false;
        });
    }

    public void FadeOutMenu(GameObject menu)
    {
        swapingMenus = true;

        RectTransform menuRect = menu.GetComponent<RectTransform>();
        Vector2 intialPos = menuRect.anchoredPosition;
        Vector2 tgtPos = menuRect.anchoredPosition + GetFadeTargetPosition(menuRect);

        SetAlpha(menu.GetComponent<CanvasGroup>(), 1, 0);
        LeanTween.value(menu, (vec) =>
        {
            menuRect.anchoredPosition = vec;
        }, intialPos, tgtPos, fadeTime).setOnComplete(() =>
        {
            swapingMenus = false;
            menu.SetActive(false);
        });
    }

    public void FadeOutMenu(GameObject menu, Action _OnFadeEnd)
    {
        swapingMenus = true;

        RectTransform menuRect = menu.GetComponent<RectTransform>();
        Vector2 intialPos = menuRect.anchoredPosition;
        Vector2 tgtPos = menuRect.anchoredPosition + GetFadeTargetPosition(menuRect);

        SetAlpha(menu.GetComponent<CanvasGroup>(), 1, 0);
        LeanTween.value(menu, (vec) =>
        {
            menuRect.anchoredPosition = vec;
        }, intialPos, tgtPos, fadeTime).setOnComplete(() =>
        {
            swapingMenus = false;
            menu.SetActive(false);
            _OnFadeEnd?.Invoke();
        });
    }

    public void FadeSwapMenu(GameObject current, GameObject next, Action _OnFadeEnd)
    {
        fadeDirection = 1;
        FadeInMenu(next);
        FadeOutMenu(current, _OnFadeEnd);
    }
    #endregion
}