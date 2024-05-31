using UnityEngine;
using UnityEngine.EventSystems;

public class MenuAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject menu;
    private Animation menuAnimation;
    void Start()
    {
        menuAnimation = menu.GetComponent<Animation>();
        menu.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        menu.SetActive(true);
        PlaySlideOutAnimation();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // Если SlideOut в данный момент не проигрывается, проиграйте его
        if (!menuAnimation.isPlaying)
        {
            PlaySlideOutAnimation();
        }
    }
    void PlaySlideOutAnimation()
    {
        menuAnimation.Play("SlideOut");
        // Остановка всех анимаций после завершения анимации SlideOut
        Invoke("StopAllAnimations", menuAnimation["SlideOut"].length);
    }
    void StopAllAnimations() => menuAnimation.Stop();

}