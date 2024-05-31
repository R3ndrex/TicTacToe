using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WithBotS�reenMain : BaseMenu
{
    [SerializeField] private AudioSource soundPlayer;
    [SerializeField] private ComputerScreenGameManager gameManager;
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

    private IEnumerator RetryAfterSound()
    {
        yield return new WaitWhile(() => soundPlayer.isPlaying);
        yield return new WaitForSeconds(0.5f);
        gameManager.SetDefault();
    }
}
