using UnityEngine;
using UnityEngine.UI;

public class ToggleDifficulty : MonoBehaviour
{
    [SerializeField] private Toggle toggleHard;
    [SerializeField] private Toggle toggleEasy;
    private const string DIFFICULTY = "Difficulty";
    void Start()
    {
        if (PlayerPrefs.HasKey(DIFFICULTY))
        {
            string difficulty = PlayerPrefs.GetString(DIFFICULTY);
            switch (difficulty)
            {
                case "Easy":
                    {
                        toggleEasy.isOn = true;
                        break;
                    }
                case "Hard":
                    {
                        toggleHard.isOn = true;
                        break;
                    }
            }
        }
    }
    private void SetPlayerPrefs(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }
    public void ToggleHard() => SetPlayerPrefs(DIFFICULTY, "Hard");
    public void ToggleEasy() => SetPlayerPrefs(DIFFICULTY, "Easy");
}
