using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SwitchView : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isOn;
    public UnityEvent<bool> onSwitchToggled;

    private Animator _animator;
    private static readonly int Pressed = Animator.StringToHash("Pressed");
    private static readonly int Normal = Animator.StringToHash("Normal");
    private static readonly int IsOn = Animator.StringToHash("isOn");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (_animator == null) return;
        
        _animator.SetTrigger(Normal);
        _animator.SetBool(IsOn, isOn);
    }

    public void SetState(bool switchIsOn)
    {
        isOn = switchIsOn;
        if (_animator != null)
        {
            _animator.SetBool(IsOn, switchIsOn);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    private void Toggle()
    {
        isOn = !isOn;
        onSwitchToggled?.Invoke(isOn);
        if (_animator != null)
        {
            _animator.SetBool(IsOn, isOn);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_animator != null)
        {
            _animator.SetTrigger(Pressed);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_animator != null)
        {
            _animator.SetTrigger(Normal);
        }
    }
}
