using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlidersChange : MonoBehaviour
{
    public static VolumeSlidersChange instance;
    [SerializeField] private Slider masterVolume, sfxVolume, musicVolume;
    [SerializeField] private AudioMixer mainAudioMixer;

    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";


    void Start() => LoadVolumes();

    public void SetMasterVolume() => SetVolume("MasterVolume", masterVolume);

    public void SetSFXVolume() => SetVolume("SFXVolume", sfxVolume);

    public void SetMusicVolume() => SetVolume("MusicVolume", musicVolume);

    public void SetVolume(string volumeName, Slider slider)
    {
        float volumeInDb = Mathf.Log10(slider.value) * 20;
        mainAudioMixer.SetFloat(volumeName, volumeInDb);
        PlayerPrefs.SetFloat(volumeName, slider.value);
        PlayerPrefs.Save();
    }

    public void LoadVolumes()
    {
        if (PlayerPrefs.HasKey(MASTER_VOLUME_KEY))
            masterVolume.value = PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
        if (PlayerPrefs.HasKey(SFX_VOLUME_KEY))
            sfxVolume.value = PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
        if (PlayerPrefs.HasKey(MUSIC_VOLUME_KEY))
            musicVolume.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY);
    }
}
