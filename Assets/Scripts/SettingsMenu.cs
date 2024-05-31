using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : BaseMenu
{
    [SerializeField] private AudioSource soundPlayer;
    [SerializeField] private Toggle toggleXPlayer;
    [SerializeField] private Toggle toggleOPlayer;
    [SerializeField] private Toggle toggleRandomPlayer;
    [SerializeField] private Toggle toggleXFirstTurn;
    [SerializeField] private Toggle toggleOFirstTurn;
    [SerializeField] private Toggle toggleRandomFirstTurn;

    private const string PLAYER_SYMBOL = "Player_Symbol";
    private const string FIRST_TURN = "First_Turn";

    private void Start() => LoadGameSettings();

    private void LoadGameSettings()
    {
        LoadPlayerPrefs(PLAYER_SYMBOL, toggleXPlayer, toggleOPlayer, toggleRandomPlayer);
        LoadPlayerPrefs(FIRST_TURN, toggleXFirstTurn, toggleOFirstTurn, toggleRandomFirstTurn);
    }

    private void LoadPlayerPrefs(string key, Toggle toggle1, Toggle toggle2, Toggle toggle3)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string savedSymbol = PlayerPrefs.GetString(key);
            switch (savedSymbol)
            {
                case Symbol.O:
                    toggle2.isOn = true;
                    break;
                case Symbol.Random:
                    toggle3.isOn = true;
                    break;
                default:
                    toggle1.isOn = true;
                    break;
            }
        }

    }
    public void ReturnToMainMenu() => StartCoroutine(LoadSceneAfterSound("MainMenu"));

    private IEnumerator LoadSceneAfterSound(string sceneName)
    {
        soundPlayer.Play();
        yield return new WaitWhile(() => soundPlayer.isPlaying);
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ToggleX() => SetPlayerPrefs(PLAYER_SYMBOL, Symbol.X);
    public void ToggleO() => SetPlayerPrefs(PLAYER_SYMBOL, Symbol.O);
    public void ToggleRandomSymbol() => SetPlayerPrefs(PLAYER_SYMBOL, Symbol.Random);
    public void ToggleXFirstTurn() => SetPlayerPrefs(FIRST_TURN, Symbol.X);
    public void ToggleOFirstTurn() => SetPlayerPrefs(FIRST_TURN, Symbol.O);
    public void ToggleRandomFirstTurn() => SetPlayerPrefs(FIRST_TURN, Symbol.Random);

    private void SetPlayerPrefs(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }
}
