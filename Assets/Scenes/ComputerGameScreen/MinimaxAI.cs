using System;
public class MinimaxAI
{
    private char playerSymbol;
    private char aiSymbol;
    public MinimaxAI(char playerSymbol)
    {
        this.playerSymbol = playerSymbol;
        aiSymbol = playerSymbol == 'X' ? 'O' : 'X';
    }
    private int MiniMax(char[] _board, bool maximazing, int depth)
    {
        char[] board = (char[])_board.Clone();

        if (HasWon(board, aiSymbol))
            return 10;
        else if (HasWon(board, playerSymbol))
            return -10;
        else if (DrawCheck(board))
            return 0;

        if (maximazing)
        {
            int max = int.MinValue;
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == ' ')
                {
                    int returner = MiniMax(MakeMoveAtSlot(board, false, i), false, depth + 1);
                    max = Math.Max(max, returner - depth);
                }
            }
            return max;
        }
        else
        {
            int min = int.MaxValue;
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == ' ')
                {
                    int returner = MiniMax(MakeMoveAtSlot(board, true, i), true, depth + 1);
                    min = Math.Min(min, returner);
                }
            }
            return min;
        }
    }
    char[] MakeMoveAtSlot(char[] _board, bool whosMoving, int slot)
    {
        char[] board = (char[])_board.Clone();
        board[slot] = whosMoving ? playerSymbol : aiSymbol;
        return board;
    }
    public int BestMove(char[] _board)
    {
        int max = int.MinValue;
        int slot = int.MinValue;
        char[] board = (char[])_board.Clone();
        for (int i = 0; i < board.Length; i++)
            if (board[i] == ' ')
            {
                int returner = MiniMax(MakeMoveAtSlot(board, false, i), false, 0);
                if (returner > max)
                {
                    max = returner;
                    slot = i;
                }
            }
        return slot;
    }
    private static bool HasWon(char[] board, char symbol)
    {
        var t = symbol;
        return // Rows
           (board[0] == t && board[1] == t && board[2] == t) ||
           (board[3] == t && board[4] == t && board[5] == t) ||
           (board[6] == t && board[7] == t && board[8] == t) ||
           // Columns
           (board[0] == t && board[3] == t && board[6] == t) ||
           (board[1] == t && board[4] == t && board[7] == t) ||
           (board[2] == t && board[5] == t && board[8] == t) ||
           // Diagonals
           (board[0] == t && board[4] == t && board[8] == t) ||
           (board[2] == t && board[4] == t && board[6] == t);
    }
    private static bool DrawCheck(char[] board)
    {
        for (int i = 0; i < 9; i++)
            if (board[i] == ' ')
                return false;
        return true;
    }
}