using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoloScreenMain : BaseMenu
{
    [SerializeField] private AudioSource soundPlayer;
    [SerializeField] private LocalGameManager gameManager;

    protected override void CheckEscapeKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadSceneAsync("ModeSelection");
    }
    public void ReturnToMainMenu()
    {
        soundPlayer.Play();
        StartCoroutine(LoadSceneAfterSound("MainMenu"));
    }
    private IEnumerator LoadSceneAfterSound(string sceneName)
    {
        yield return new WaitWhile(() => soundPlayer.isPlaying);
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void Retry()
    {
        soundPlayer.Play();
        StartCoroutine(RetryAfterSound());
    }
    IEnumerator RetryAfterSound()
    {
        yield return new WaitWhile(() => soundPlayer.isPlaying);
        yield return new WaitForSeconds(0.2f);
        gameManager.SetDefault();
    }
}
