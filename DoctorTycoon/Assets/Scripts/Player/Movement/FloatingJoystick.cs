using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class FloatingJoystick : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _knob;
    public RectTransform RectTransform {  get { return _rectTransform; } }
    public RectTransform Knob {  get { return _knob; } }

}