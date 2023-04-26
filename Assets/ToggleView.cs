using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleView : MonoBehaviour, IPointerClickHandler
{
    public bool isOn;
    public Image targetGraphic;
    public UnityEvent<bool> onToggle;
    public Sprite spriteOn;
    public Sprite spriteOff;

    public void Start()
    {
        RefreshTargetImage();
    }

    private void RefreshTargetImage()
    {
        targetGraphic.sprite = isOn ? spriteOn : spriteOff;
    }

    private void Toggle()
    {
        isOn = !isOn;
        RefreshTargetImage();
        onToggle.Invoke(isOn);
    }

    public void SetIsOn(bool isOn)
    {
        this.isOn = isOn;
        RefreshTargetImage();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }
}
