using TMPro;
using UnityEngine;

public class VersionDisplay: MonoBehaviour
{
    private TextMeshProUGUI versionText;

    void Start()
    {
        versionText = GetComponent<TextMeshProUGUI>();

        if (Application.version != null)
            versionText.text = $"Version: <color=#61e8ff>{Application.version}</color>";
    }
}