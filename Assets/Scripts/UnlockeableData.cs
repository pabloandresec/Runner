using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "gameData", menuName = "GameData/GameData")]
public class UnlockeableData : ScriptableObject
{
    [SerializeField] private SavedData data;

    public SavedData Data { get => data; }

    public void LoadData()
    {
        string s = PlayerPrefs.GetString("data", "");
        if(s != "")
        {
            SavedData d = JsonUtility.FromJson<SavedData>(s);
            data = d;
            Debug.Log("DATA LOADED");
        }
        else
        {
            Debug.Log("No data in storage");
        }
    }

    public void SaveData()
    {
        string s = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("data", s);
        Debug.Log("DATA SAVED");
    }

    public void AddVariation(string layerA, string layerB)
    {
        if(data.UnlockedVariations == null)
        {
            data.UnlockedVariations = new List<string>();
        }
        data.UnlockedVariations.Add(layerA + "_" + layerB);
    }
}

[Serializable]
public class SavedData
{
    [SerializeField] List<string> unlockedVariations;
    [SerializeField] float sfxVol;
    [SerializeField] float musicVol;
    [SerializeField] int progress;

    public List<string> UnlockedVariations { get => unlockedVariations; set => unlockedVariations = value; }
    public float SfxVol { get => sfxVol; set => sfxVol = value; }
    public float MusicVol { get => musicVol; set => musicVol = value; }
    public int Progress { get => progress; set => progress = value; }
}
