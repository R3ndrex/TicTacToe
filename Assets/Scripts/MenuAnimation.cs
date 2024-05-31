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
        // ���� SlideOut � ������ ������ �� �������������, ���������� ���
        if (!menuAnimation.isPlaying)
        {
            PlaySlideOutAnimation();
        }
    }
    void PlaySlideOutAnimation()
    {
        menuAnimation.Play("SlideOut");
        // ��������� ���� �������� ����� ���������� �������� SlideOut
        Invoke("StopAllAnimations", menuAnimation["SlideOut"].length);
    }
    void StopAllAnimations() => menuAnimation.Stop();

}