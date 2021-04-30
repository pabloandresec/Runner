using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Profession Appearance", menuName = "Profession Appearance")]
public class ProfessionAppearanceData : ScriptableObject
{
    [SerializeField] private string professionName = null;
    [SerializeField] private Sprite[] skins = null;
    [SerializeField] private Sprite[] hairs = null;

    public Sprite[] Skins { get => skins; }
    public Sprite[] Hairs { get => hairs; }
    public string ProfessionName { get => professionName; set => professionName = value; }
}
