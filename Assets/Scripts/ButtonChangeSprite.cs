using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonChangeSprite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;
    [SerializeField] private AudioSource soundPlayer;
    [SerializeField] private Sprite defaultSprite;
    private BaseGameManager myManager;
    private Color originalColor;
    private void Awake() => originalColor = buttonImage.color;
    private void Play()
    {
        if (soundPlayer != null)
        {
            soundPlayer.Play();
        }
        else
        {
            Debug.Log("SoundPlayer is null");
        }
    }
    public void SetController(BaseGameManager manager) => myManager = manager;
    public void OnPressedButton()
    {
        if (button != null && button.interactable)
        {
            buttonImage.sprite = myManager.GetPlayerSide();
            Color color = buttonImage.color;
            color.a = 1f;
            buttonImage.color = color;
            button.interactable = false;
            myManager.EndTurn();
            Play();
        }
    }
    public void UndoPressedButton()
    {
        Color color = buttonImage.color;
        color.a = 0f;
        buttonImage.color = color;
        buttonImage.sprite = defaultSprite;
        button.interactable = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            if (myManager != null && myManager.GetPlayerSide() != null && buttonImage.sprite == defaultSprite)
            {
                buttonImage.sprite = myManager.GetPlayerSide();
            }
            Color color = buttonImage.color;
            color.a = 0.5f;
            buttonImage.color = color;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (button != null && button.interactable)
        {
            if (myManager != null && buttonImage.sprite == myManager.GetPlayerSide())
            {
                buttonImage.sprite = defaultSprite;
            }
            buttonImage.color = originalColor;
        }
    }
}