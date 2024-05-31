using UnityEngine;
using UnityEngine.UI;

public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] GameObject winPanel;
    private void Awake()
    {
        BaseGameManager.OnGameOver += DisplayWinPanel;
        BaseGameManager.OnGameOver += DisplayWinPanelText;
    }
    void DisplayWinPanel(string text)
    {
        if(winPanel != null)
            winPanel.SetActive(true);
    }
    void DisplayWinPanelText(string text)
    {
        if (winPanel != null)
            winPanel.GetComponentInChildren<Text>().text = text;
    }
}
