﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingChooser : MonoBehaviour
{
    [SerializeField] private PersistentData data;
    [SerializeField] private List<ProfessionCombinations> combinations;
    [Header("UI")]
    [SerializeField] private Image[] layers;

    private void Start()
    {
        CheckResults();
    }

    private void CheckResults()
    {
        int[] combinationsPoints = new int[combinations.Count];
        for (int i = 0; i < combinationsPoints.Length; i++)
        {
            combinationsPoints[i] = 0;
        }


        for (int j = 0; j < combinations.Count; j++)
        {
            for (int i = 0; i < data.ProfessionPicks.Count; i++)
            {
                int total = combinations[j].Test(data.ProfessionPicks[i]);
                combinationsPoints[j] += total;
            }
        }

        int selectedIndex = 0;
        int maxVal = 0;
        for (int x = 0; x < combinationsPoints.Length; x++)
        {
            if(combinationsPoints[x] > maxVal)
            {
                maxVal = combinationsPoints[x];
                selectedIndex = x;
            }
            Debug.Log(combinations[x].ProfessionName + " points = " + combinationsPoints[x]);
        }

        Debug.Log("Selected profession "+ combinations[selectedIndex].ProfessionName + " with " + combinationsPoints[selectedIndex]);

        SelectEpilogue(combinations[selectedIndex]);
    }

    private void SelectEpilogue(ProfessionCombinations professionCombinations)
    {
        throw new NotImplementedException();
    }

}
