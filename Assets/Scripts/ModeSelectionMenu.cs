using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelectionMenu : BaseMenu
{
    [SerializeField] private AudioSource soundPlayer;

    public void PlaySolo() => PlaySoundAndLoadScene("SoloScreen");

    public void PlayWithBot() => PlaySoundAndLoadScene("WithBotScreen");

    private void PlaySoundAndLoadScene(string sceneName)
    {
        soundPlayer.Play();
        StartCoroutine(LoadSceneAfterSound(sceneName));
    }

    private IEnumerator LoadSceneAfterSound(string sceneName)
    {
        yield return new WaitWhile(() => soundPlayer.isPlaying);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
