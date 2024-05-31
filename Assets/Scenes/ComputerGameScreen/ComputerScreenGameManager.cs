using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerScreenGameManager : BaseGameManager
{
    [SerializeField] private Image[] buttonImages;
    [SerializeField] private Sprite clickedSpriteX;
    [SerializeField] private Sprite clickedSpriteO;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TMP_Text textTurns;
    [SerializeField] private MinimaxAI ai;
    private Sprite playingSide;
    private string playerSymbol;
    private string firstTurnSymbol;
    private bool isFirstTurn = true;
    private const string DIFFICULTY_KEY = "Difficulty";
    private const string HARDMODE_KEY = "Hard";
    private char[] board;

    private void Awake()
    {
        try
        {
            GamePreferences = new GamePreferences();
            ButtonScripts = new ButtonChangeSprite[buttonImages.Length];
            for (int i = 0; i < buttonImages.Length; i++)
            {
                ButtonScripts[i] = buttonImages[i].GetComponent<ButtonChangeSprite>();
            }
            WinPatterns = GetWinPatterns(buttonImages);
            SetControllerOnButtons(buttonImages);
            SetDefault();
            board = new char[buttonImages.Length];
        }
        catch(Exception ex)
        { 
            Debug.Log($"ComputerScreenGameManager Awake Error:{ex.Message}");
        }
    }

    public override void SetDefault()
    {
        firstTurnSymbol = GamePreferences.FirstTurnSymbol;
        playerSymbol = GamePreferences.PlayerSymbol;
        if (firstTurnSymbol == Symbol.Random)
        {
            firstTurnSymbol = GamePreferences.GetRandomSymbol();
        }
        if (playerSymbol == Symbol.Random)
        {
            playerSymbol = GamePreferences.GetRandomSymbol();
        }
        if (firstTurnSymbol == playerSymbol)
        {
            ai = new MinimaxAI(playerSymbol[0]);
        }
        else
        {
            string opponent = playerSymbol == Symbol.X ? Symbol.O : Symbol.X;
            ai = new MinimaxAI(opponent[0]);
        }
        moveCount = 0;
        winScreen.SetActive(false);
        isFirstTurn = true;

        for (int i = 0; i < buttonImages.Length; i++)
        {
            ButtonScripts[i].UndoPressedButton();
        }
        playingSide = GamePreferences.HasPlayerSymbolPref switch
        {
            true => (playerSymbol == Symbol.O) ? clickedSpriteO : clickedSpriteX,
            _ => clickedSpriteX,
        };
        textTurns.text = GetTurnText(playingSide);
        if (isFirstTurn && playerSymbol != firstTurnSymbol)
        {
            playingSide = (firstTurnSymbol == Symbol.X) ? clickedSpriteX : clickedSpriteO;
            isFirstTurn = false;
            ComputerTurn();
        }

    }
    private string GetTurnText(Sprite side) => side == clickedSpriteX ? $"{Symbol.X}'s Turn" : $"{Symbol.O}'s Turn";
    public override Sprite GetPlayerSide() => playingSide;
    public override void EndTurn()
    {
        CheckForWin();
        moveCount++;

        if (!IsGameOver() && moveCount >= buttonImages.Length)
        {
            OnGameOver?.Invoke("Draw!");
            return;
        }
        ChangeSides();
        if (IsComputerTurn())
        {
            ComputerTurn();
            return;
        }
    }
    private bool IsComputerTurn()
    {
        if (moveCount % 2 != 0 && playerSymbol == firstTurnSymbol)
        {
            return true;
        }
        if (moveCount % 2 == 0 && playerSymbol != firstTurnSymbol)
        {
            return true;
        }
        return false; // Сейчас ход игрока
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
    private void ComputerTurn()
    {
        if (PlayerPrefs.GetString(DIFFICULTY_KEY) == HARDMODE_KEY)
        {
            for (int i = 0; i < buttonImages.Length; i++)
            {
                if (buttonImages[i].sprite == defaultSprite)
                    board[i] = ' ';
                else if (buttonImages[i].sprite == clickedSpriteO)
                    board[i] = 'O';
                else if (buttonImages[i].sprite == clickedSpriteX)
                    board[i] = 'X';
            }

            int bestMove = ai.BestMove(board);
            ButtonScripts[bestMove].OnPressedButton();
        }
        else
            RandomTurn();
    }
    private bool IsGameOver() => winScreen.activeSelf;
    private void RandomTurn()
    {
        bool foundEmptySpot = false;
        while (!foundEmptySpot)
        {
            int randomNumber = UnityEngine.Random.Range(0, buttonImages.Length);
            if (buttonImages[randomNumber].GetComponent<Button>().interactable)
            {
                ButtonScripts[randomNumber].OnPressedButton();
                foundEmptySpot = true;
            }
        }
    }
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
}