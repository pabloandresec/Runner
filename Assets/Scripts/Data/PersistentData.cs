using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "PersistentData", fileName = "Data")]
public class PersistentData : ScriptableObject
{
    [SerializeField] private string currentCombination = "";
    [SerializeField] private List<PickAmountPair> professionPicks;

    public string CurrentCombination { get => currentCombination; set => currentCombination = value; }

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
