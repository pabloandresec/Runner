﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelType levelType = LevelType.APPEARANCE_SELECT_LEVEL;
    [SerializeField] private PersistentData gameData;
    [SerializeField] private CharacterAppearanceHandler player;
    [SerializeField] private Transform layersParent;

    private PickProfession[] professionPicks;
    private Pick[] picks;

    private void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAppearanceHandler>();
        }
        gameData.SetLayersAmount(layersParent.childCount);
        gameData.LoadPlayer(player);

        professionPicks = FindObjectsOfType<PickProfession>();
        if(professionPicks.Length > 0)
        {
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
            AppearanceWrapper tempAppear = new AppearanceWrapper(i, layersParent.GetChild(i).GetComponent<Animator>().runtimeAnimatorController, layersParent.GetChild(i).GetComponent<SpriteRenderer>().color);
            gameData.SetAppearance(i, tempAppear);
        }
        p.AppearanceUpdated -= OnPickedAppearance;
    }

    private void OnPickedProfession(string pickID, PickProfession p)
    {
        gameData.AddNewPick(pickID);
        p.onPickedItem -= OnPickedProfession;
    }
}

public enum LevelType
{
    PROFESSION_SELECT_LEVEL,
    APPEARANCE_SELECT_LEVEL,
    OTHER
}
