using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingChooserB : MonoBehaviour
{
    [SerializeField] private PersistentData data;
    [SerializeField] private List<ProfessionAppearanceData> professions;
    [Header("UI")]
    [SerializeField] private Image[] layers;
    [SerializeField] private TextMeshProUGUI[] texts;

    private void Start()
    {
        CheckResults();

    }

    private void CheckResults()
    {
        PickAmountPair p = data.ProfessionPicks.FirstOrDefault(pp => pp.amount >= 3);
        ProfessionAppearanceData profession = professions.FirstOrDefault(c => c.ProfessionName == p.itemName);

        if(profession != null)
        {
            Debug.Log("Selected profession " + profession.ProfessionName);
            SelectEpilogue(profession);
        }
        else
        {
            Debug.Log("Selected profession non existant");
        }
    }

    private void SelectEpilogue(ProfessionAppearanceData profession)
    {
        layers[0].sprite = SelectSkin(profession);
        if(layers[0].sprite == null)
        {
            Debug.LogWarning("No base selected");
        }
        if(data.Layers[1].AppearanceController != null)
        {
            layers[1].sprite = SelectHair(profession);
            if (layers[1].sprite == null)
            {
                Debug.LogWarning("No hair available");
                layers[1].gameObject.SetActive(false);
            }
            else
            {
                layers[1].color = data.Layers[1].Color;
                layers[1].gameObject.SetActive(true);
            }
        }
    }

    private Sprite SelectSkin(ProfessionAppearanceData profession)
    {
        string[] parsedName = data.Layers[0].AppearanceController.name.Split('_');
        string letter = parsedName[parsedName.Length - 1];

        foreach (Sprite s in profession.Skins)
        {
            if(letter.ToUpper().Contains(s.name.ToUpper()))
            {
                return s;
            }
        }
        return null;
    }

    private Sprite SelectHair(ProfessionAppearanceData profession)
    {
        string[] n = data.Layers[1].AppearanceController.name.Split('_');
        Debug.Log(n[n.Length - 1]);

        foreach (Sprite s in profession.Hairs)
        {
            string sname = s.name;
            if (sname.Contains(n[n.Length - 1]))
            {
                return s;
            }
        }
        return null;
    }
}
