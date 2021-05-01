using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelType levelType = LevelType.APPEARANCE_SELECT_LEVEL;
    [SerializeField] private PersistentData gameData;
    [SerializeField] private CharacterAppearanceHandler player;
    [SerializeField] private Transform layersParent;

    private PickProfession[] professionPicks;
    private Pick[] picks;
    private int pickedItems = 0;
    private Dictionary<String, int> pickedItemsAmount;

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAppearanceHandler>();
        }
        gameData.SetLayersAmount(layersParent.childCount);
        

        professionPicks = FindObjectsOfType<PickProfession>();
        if(professionPicks.Length > 2) // Esto significa que esta en el nivel de professiones
        {
            gameData.LoadPlayer(player);
            pickedItems = 0;
            pickedItemsAmount = new Dictionary<string, int>();
            gameData.ClearPicks();
            foreach (PickProfession p in professionPicks)
            {
                p.onPickedItem += OnPickedProfession;
            }
        }

        picks = FindObjectsOfType<Pick>();
        foreach (Pick p in picks)
        {
            p.AppearanceUpdated += OnPickedAppearance;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadMultiply))
        {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnDisable()
    {
        if(professionPicks.Length > 0)
        {
            foreach (PickProfession p in professionPicks)
            {
                p.onPickedItem -= OnPickedProfession;
            }
        }
        
        foreach (Pick p in picks)
        {
            p.AppearanceUpdated -= OnPickedAppearance;
        }
    }

    private void OnPickedAppearance(Pick p)
    {
        for (int i = 0; i < layersParent.childCount; i++)
        {
            AppearanceWrapper tempAppear = new AppearanceWrapper(i, layersParent.GetChild(i).GetComponent<Animator>().runtimeAnimatorController, layersParent.GetChild(i).GetComponent<SpriteRenderer>().color, "Black");
            gameData.SetAppearance(i, tempAppear);
        }
        p.AppearanceUpdated -= OnPickedAppearance;
    }

    private void OnPickedProfession(string pickID, PickProfession p)
    {
        gameData.AddNewPick(pickID);
        p.onPickedItem -= OnPickedProfession;

        if(pickedItemsAmount.ContainsKey(pickID))
        {
            int val = pickedItemsAmount[pickID];
            Debug.Log("El sistema ya tenia " + val + " picks de " + pickID);
            val++;
            pickedItemsAmount[pickID] = val;
            Debug.Log("El sistema tiene en total " +val + " picks de " + pickID);
        }
        else
        {
            Debug.Log("El sistema ha agregado " + pickID);
            pickedItemsAmount.Add(pickID, 1);
        }

        foreach (KeyValuePair<string,int> pairPick in pickedItemsAmount)
        {
            if(pairPick.Value >= 3)
            {
                GameObject.FindGameObjectWithTag("UI").GetComponent<GameUI>().LoadScene(3);
                return;
            }
        }
    }

    public void EnableDirectChilds(Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            t.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void DisableDirectChilds(Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            t.GetChild(i).gameObject.SetActive(false);
        }
    }
}

public enum LevelType
{
    PROFESSION_SELECT_LEVEL,
    APPEARANCE_SELECT_LEVEL,
    OTHER
}
