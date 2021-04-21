﻿using System;
using UnityEngine;
using System.Linq;

[Serializable]
public class ProfessionCombinations
{
    [SerializeField] private string professionName = "No name";
    [SerializeField] private MinMaxValue[] values;

    public string ProfessionName { get => professionName; set => professionName = value; }

    public int Test(PickAmountPair pickAmountPair)
    {
        MinMaxValue val = values.FirstOrDefault(v => v.id == pickAmountPair.itemName);
        if(val == null || pickAmountPair.amount == 0)
        {
            return 0;
        }
        else
        {
            if(pickAmountPair.amount > val.Min && pickAmountPair.amount < val.Max)
            {
                return 10;
            }
            else if(pickAmountPair.amount < val.Min && pickAmountPair.amount > 0)
            {
                return 1;
            }
            else
            {
                return 5;
            }
        }
    }
}