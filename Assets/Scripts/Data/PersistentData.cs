using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "PersistentData", fileName = "Data")]
public class PersistentData : ScriptableObject
{
    [SerializeField] private AppearanceWrapper[] layers;
    [SerializeField] private List<PickAmountPair> professionPicks;

    public AppearanceWrapper[] Layers { get => layers; }
    public List<PickAmountPair> ProfessionPicks { get => professionPicks; }

    public void SetLayersAmount(int i)
    {
        if(layers == null || layers.Length != i)
        {
            layers = new AppearanceWrapper[i];
            Debug.Log("Appearance save init");
            return;
        }
        Debug.Log("Appearance saver already has values");
    }

    public void LoadPlayer(CharacterAppearanceHandler player)
    {
        if(layers[0].AppearanceController == null)
        {
            Debug.LogWarning("Save has not a base layer");
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            player.SwapAppearance(layers[i]);
        }
    }

    public void AddNewPick(string newPick)
    {
        if(professionPicks == null)
        {
            professionPicks = new List<PickAmountPair>();
        }
        PickAmountPair query = professionPicks.FirstOrDefault(n => n.itemName == newPick);
        if(query == null)
        {
            professionPicks.Add(new PickAmountPair(newPick, 1));
        }
        else
        {
            query.amount++;
        }
    }

    public void ClearPicks()
    {
        professionPicks.Clear();
        Debug.Log("Picks Cleared!");
    }

    public void SetAppearance(int index, AppearanceWrapper appearance)
    {
        layers[index] = appearance;
    }
}

[Serializable]
public class PickAmountPair
{
    public string itemName;
    public int amount;

    public PickAmountPair(string itemName, int amount)
    {
        this.itemName = itemName;
        this.amount = amount;
    }
}
