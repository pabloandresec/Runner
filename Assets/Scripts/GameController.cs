using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public PersistentData gameData;

    private PickProfession[] picks;

    private void Start()
    {
        gameData.ClearPicks();

        picks = FindObjectsOfType<PickProfession>();

        foreach(PickProfession p in picks)
        {
            p.onPickedItem += OnPickedProfession;
        }
    }

    private void OnDisable()
    {
        foreach (PickProfession p in picks)
        {
            p.onPickedItem -= OnPickedProfession;
        }
    }

    private void OnPickedProfession(string pickID, PickProfession p)
    {
        gameData.AddNewPick(pickID);
        p.onPickedItem -= OnPickedProfession;
    }
}
