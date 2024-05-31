using UnityEngine;
using UnityEngine.UI;
public abstract class BaseGameManager : MonoBehaviour
{
    public delegate void OnGameOverEventHandler(string text);
    public static OnGameOverEventHandler OnGameOver;

    protected int moveCount;
    protected ButtonChangeSprite[] ButtonScripts;
    protected Image[,] WinPatterns;
    protected GamePreferences GamePreferences;
    public abstract Sprite GetPlayerSide();
    public abstract void SetDefault();
    public abstract void EndTurn();
    protected abstract void ChangeSides();
    protected void SetControllerOnButtons(Image[] buttonImages)
    {
        foreach (var button in buttonImages)
        {
            if (button.TryGetComponent<ButtonChangeSprite>(out var buttonComponent))
            {
                buttonComponent.SetController(this);
            }
        }
    }
    protected Image[,] GetWinPatterns(Image[] board)
    {
        return new Image[8, 3]
        {
            {board[0], board[1], board[2]},
            {board[3], board[4], board[5]},
            {board[6], board[7], board[8]},
            {board[0], board[3], board[6]},
            {board[1], board[4], board[7]},
            {board[2], board[5], board[8]},
            {board[0], board[4], board[8]},
            {board[2], board[4], board[6]}
        };

    }
}
