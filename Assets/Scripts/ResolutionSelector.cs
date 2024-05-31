using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionSelector : MonoBehaviour
{
    private TMP_Dropdown displayModeDropdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    private void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        displayModeDropdown = GetComponent<TMP_Dropdown>();
        displayModeDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOptions = filteredResolutions[i].width + "x" + filteredResolutions[i].height;
            options.Add(resolutionOptions);

            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        displayModeDropdown.AddOptions(options);
        displayModeDropdown.value = currentResolutionIndex;
        displayModeDropdown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        Resolution resolution = filteredResolutions[index];

        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }
}
