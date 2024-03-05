using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class FloatingJoystick : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _knob;
    private bool _canInteract = true;

    public RectTransform RectTransform {  get { return _rectTransform; } }
    public RectTransform Knob {  get { return _knob; } }

    public bool CanInteract {  get { return _canInteract; } }
    public void SwitchToFalseInteractability() => _canInteract = false;
    public void SwitchToTrueInteractability() => _canInteract = true;


}