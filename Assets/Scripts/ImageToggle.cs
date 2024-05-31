using UnityEngine;
using UnityEngine.UI;

public class ImageToggle : MonoBehaviour
{
    public Toggle toggle;
    public Sprite checkedSprite;
    public void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            toggle.image.sprite = checkedSprite;
        }
    }

    void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}
