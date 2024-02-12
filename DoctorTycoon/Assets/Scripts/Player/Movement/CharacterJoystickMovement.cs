using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class CharacterJoystickMovement : CharacterMovment
{
    [SerializeField] private Vector2 _joystickSize = new Vector2(200, 200);
    [SerializeField] private FloatingJoystick _joystick;
    private Vector3 _scaledMovement;
    private Finger _movementFinger;
    private Vector2 _movementAmount;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        TargetFace();
        Move(Agent);
    }
    public override void Move(NavMeshAgent agent)
    {
        _scaledMovement = agent.speed * Time.deltaTime * new Vector3(_movementAmount.x, 0, _movementAmount.y);
        if(_scaledMovement.x != 0 || _scaledMovement.y != 0 || _scaledMovement.z != 0)
        {
            IsWalking = true;
            agent.Move(_scaledMovement);
        }
        else
            IsWalking = false;
    }

    public override void TargetFace()
    {
        Agent.transform.LookAt((Agent.transform.position + _scaledMovement) * LookRotationSpeed, Vector3.up);
    }


    private void HandleFingerMove(Finger MovedFinger)
    {
        if (MovedFinger == _movementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = _joystickSize.x / 2f;
            ETouch.Touch currentTouch = MovedFinger.currentTouch;

            if (Vector2.Distance(
                    currentTouch.screenPosition,
                    _joystick.RectTransform.anchoredPosition
                ) > maxMovement)
            {
                knobPosition = (
                    currentTouch.screenPosition - _joystick.RectTransform.anchoredPosition
                    ).normalized
                    * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - _joystick.RectTransform.anchoredPosition;
            }

            _joystick.Knob.anchoredPosition = knobPosition;
            _movementAmount = knobPosition / maxMovement;
        }
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        if (LostFinger == _movementFinger)
        {
            _movementFinger = null;
            _joystick.Knob.anchoredPosition = Vector2.zero;
            _joystick.gameObject.SetActive(false);
            _movementAmount = Vector2.zero;
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (_movementFinger == null && TouchedFinger.screenPosition.y <= Screen.height/ 2.5f)
        {
            _movementFinger = TouchedFinger;
            _movementAmount = Vector2.zero;
            _joystick.gameObject.SetActive(true);
            _joystick.RectTransform.sizeDelta = _joystickSize;
            _joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        }
    }

    private Vector2 ClampStartPosition(Vector2 StartPosition)
    {
        if (StartPosition.x < _joystickSize.x / 2)
        {
            StartPosition.x = _joystickSize.x / 2;
        }

        if (StartPosition.y < _joystickSize.y / 2)
        {
            StartPosition.y = _joystickSize.y / 2;
        }
        else if (StartPosition.y > Screen.height - _joystickSize.y / 2)
        {
            StartPosition.y = Screen.height - _joystickSize.y / 2;
        }

        return StartPosition;
    }



}