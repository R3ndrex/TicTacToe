using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenModeSelector : MonoBehaviour
{
    private TMP_Dropdown displayModeScreen;
    private List<FullScreenMode> allModes;
    private static string savedScreenModeKey = "SavedScreenMode"; 

    private void Start()
    {
        displayModeScreen = GetComponent<TMP_Dropdown>();
        displayModeScreen.ClearOptions();

        allModes = new List<FullScreenMode>
        {
            FullScreenMode.FullScreenWindow,
            FullScreenMode.ExclusiveFullScreen,
            FullScreenMode.Windowed,
            FullScreenMode.MaximizedWindow
        };

        List<string> options = new List<string>();
        foreach (var mode in allModes)
        {
            options.Add(mode.ToString());
        }
        displayModeScreen.AddOptions(options);
        displayModeScreen.onValueChanged.AddListener(SetScreenMode);

        // Загрузка сохраненного режима экрана при запуске
        if (PlayerPrefs.HasKey(savedScreenModeKey))
        {
            int savedIndex = PlayerPrefs.GetInt(savedScreenModeKey);
            displayModeScreen.value = savedIndex;
            SetScreenMode(savedIndex);
        }
    }

    public void SetScreenMode(int index)
    {
        PlayerPrefs.SetInt(savedScreenModeKey, index); 

        Resolution maxResolution = Screen.resolutions[Screen.resolutions.Length - 1];
        switch (index)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 3:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
        }
    }
}