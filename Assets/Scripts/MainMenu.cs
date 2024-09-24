using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : BaseMenu
{
    [SerializeField] private AudioSource soundPlayer;
    [SerializeField] private AudioMixer mainAudioMixer;
    [SerializeField] private GameObject panel;
    private const string MASTER_VOLUME_PARAM = "MasterVolume";
    private const string SFX_VOLUME_PARAM = "SFXVolume";
    private const string MUSIC_VOLUME_PARAM = "MusicVolume";
    private void Start() => LoadSavedAudioSettings();
    protected override void CheckEscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            panel.SetActive(true);
    }
    private void LoadSavedAudioSettings()
    {

        mainAudioMixer.SetFloat(MASTER_VOLUME_PARAM, Mathf.Log10(PlayerPrefs.GetFloat(MASTER_VOLUME_PARAM))*20);
        mainAudioMixer.SetFloat(SFX_VOLUME_PARAM, Mathf.Log10(PlayerPrefs.GetFloat(SFX_VOLUME_PARAM))*20);
        mainAudioMixer.SetFloat(MUSIC_VOLUME_PARAM, Mathf.Log10(PlayerPrefs.GetFloat(MUSIC_VOLUME_PARAM)) * 20);
    }

    public void PlayGame() => PlaySoundAndLoadScene("ModeSelection");
    public void GoToSettings() => PlaySoundAndLoadScene("SettingsMenu");
    void PlaySoundAndLoadScene(string sceneName) => StartCoroutine(LoadSceneAfterSound(sceneName));
    public void PlaySound() => soundPlayer.Play();
    IEnumerator LoadSceneAfterSound(string sceneName)
    {
        PlaySound();
        yield return new WaitWhile(() => soundPlayer.isPlaying);
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void PlaySoundAndQuit() => StartCoroutine(QuitAfterSound());
    IEnumerator QuitAfterSound()
    {
        PlaySound();
        yield return new WaitWhile(() => soundPlayer.isPlaying);
        yield return new WaitForSeconds(0.2f);
        Application.Quit();
    }
    public void PlaySoundAndCancel() => StartCoroutine(CancelAfterSound());
    IEnumerator CancelAfterSound()
    {
        PlaySound();
        yield return new WaitWhile(() => soundPlayer.isPlaying);
        yield return new WaitForSeconds(0.2f);
        panel.SetActive(false);
    }
}