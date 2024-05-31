using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalGameManager : BaseGameManager
{
    [SerializeField] private Image[] buttonImages;
    [SerializeField] private Sprite clickedSpriteX;
    [SerializeField] private Sprite clickedSpriteO;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TMP_Text textTurns;
    private Sprite playingSide;
    private string playerSymbol;
    private void Awake()
    {
        GamePreferences = new GamePreferences();
        ButtonScripts = new ButtonChangeSprite[buttonImages.Length];
        for (int i = 0; i < buttonImages.Length; i++)
        {
            ButtonScripts[i] = buttonImages[i].GetComponent<ButtonChangeSprite>();
        }
        WinPatterns = GetWinPatterns(buttonImages);
        SetDefault();
        SetControllerOnButtons(buttonImages);
    }
    public override void SetDefault()
    {
        playerSymbol = GamePreferences.FirstTurnSymbol;

        if (playerSymbol == Symbol.Random)
        {
            playerSymbol = GamePreferences.GetRandomSymbol();
        }

        moveCount = 0;

        winScreen.SetActive(false);

        playingSide = GamePreferences.HasFirstTurnPref switch
        {
            true => (playerSymbol == Symbol.O) ? clickedSpriteO : clickedSpriteX,
            _ => clickedSpriteX,
        };

        textTurns.text = GetTurnText(playingSide);

        for (int i = 0; i < buttonImages.Length; i++)
        {
            ButtonScripts[i].UndoPressedButton();
        }
    }

    public override Sprite GetPlayerSide() => playingSide;
    public override void EndTurn()
    {
        CheckForWin();
        moveCount++;
        if (!IsGameOver() && moveCount >= buttonImages.Length)
        {
            OnGameOver.Invoke("Draw!");
            return;
        }
        ChangeSides();
    }
    private string GetTurnText(Sprite side) => side == clickedSpriteX ? $"{Symbol.X}'s Turn" : $"{Symbol.O}'s Turn";
    private void GameOver()
    {
        string winnerText = (playingSide == clickedSpriteX) ? $"{Symbol.X} Wins!" : $"{Symbol.O} Wins!";
        OnGameOver?.Invoke(winnerText); // Display winscreen and winner text
    }
    protected override void ChangeSides()
    {
        playingSide = (playingSide == clickedSpriteX) ? clickedSpriteO : clickedSpriteX;
        textTurns.text = GetTurnText(playingSide);
    }
    private void CheckForWin()
    {
        for (int i = 0; i < WinPatterns.GetLength(0); i++) //check if playing side has a win pattern (row, column, etc)
        {
            if (WinPatterns[i, 0].sprite.name == playingSide.name &&
                WinPatterns[i, 1].sprite.name == playingSide.name &&
                WinPatterns[i, 2].sprite.name == playingSide.name)
            {
                GameOver();
                return;
            }
        }
    }
    private bool IsGameOver() => winScreen.activeSelf;
}