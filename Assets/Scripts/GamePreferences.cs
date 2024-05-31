using UnityEngine;
public class GamePreferences
{
    private const string FIRST_TURN = "First_Turn";
    private const string PLAYER_SYMBOL = "Player_Symbol";
    public bool HasFirstTurnPref { get; private set; }
    public bool HasPlayerSymbolPref { get; private set; }
    public string PlayerSymbol { get; private set; }
    public string FirstTurnSymbol { get; private set; }
    public GamePreferences()
    {
        HasPlayerSymbolPref = PlayerPrefs.HasKey(PLAYER_SYMBOL);
        HasFirstTurnPref = PlayerPrefs.HasKey(FIRST_TURN);
        PlayerSymbol = GetPlayerSymbol();
        FirstTurnSymbol = GetFirstTurnSymbol();
    }
    public string GetPlayerSymbol() => HasPlayerSymbolPref ? PlayerPrefs.GetString(PLAYER_SYMBOL) : Symbol.X;
    public string GetFirstTurnSymbol() => HasFirstTurnPref ? PlayerPrefs.GetString(FIRST_TURN) : Symbol.X;
    public string GetRandomSymbol() => Random.Range(0, 2) == 0 ? Symbol.X : Symbol.O;
}