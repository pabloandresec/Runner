using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "GameData/AudioSettings")]
public class AudioSettingsData : ScriptableObject
{
    [SerializeField] private float sfxVol = 1;
    [SerializeField] private float musicVol = 1;

    public float SfxVol { get => sfxVol; set => sfxVol = value; }
    public float MusicVol { get => musicVol; set => musicVol = value; }
}