using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputService : IDisposable
{
    public InputService()
    {
        EnhancedTouchSupport.Enable();
    }

    public event Action<Vector2> OnPointerDown;
    public event Action<Vector2> OnPointerUp;

    public Vector2 InputDeltaResult { get; private set; }
    public Vector2 InputPosition { get; private set; }

    public Vector2 StartTouchPosition { get; private set; }

    private Vector2 _mouseDeltaInput;
    private Vector2 _touchDeltaInput;

    private Vector2 _mousePosition;
    private Vector2 _touchPosition;

    private Control_IA _control;

    public void Init()
    {
        _control = new Control_IA();
        _control.Enable();
        _control.Map.LeftMouseButton.performed += LeftMouseButton_performed;
        _control.Map.LeftMouseButton.canceled += LeftMouseButton_canceled;
    }

    private void LeftMouseButton_performed(InputAction.CallbackContext obj) =>
        OnPointerDown?.Invoke(InputPosition);

    private void LeftMouseButton_canceled(InputAction.CallbackContext obj) =>
        OnPointerUp?.Invoke(InputPosition);

    public void Update()
    {
        HandleTouchInput();
        HandleMouseInput();
        CalculateInput();
    }

    private void HandleTouchInput()
    {
        if (Touch.activeTouches.Count >= 1 && Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Began)
        {
            StartTouchPosition = Touch.activeTouches[0].screenPosition;
            _touchPosition = Touch.activeTouches[0].screenPosition;
        }
        if (Touch.activeTouches.Count >= 1 && Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Moved)
        {
            _touchDeltaInput = Touch.activeTouches[0].delta;
            _touchPosition = Touch.activeTouches[0].screenPosition;
        }
        else if (Touch.activeTouches.Count >= 1 && Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Canceled)
        {
            Debug.Log("TouchPhase.Canceled");
        }
        else
        {
            _touchDeltaInput = Vector2.zero;
            //_touchPosition = Vector2.zero;
        }
    }

    private void HandleMouseInput()
    {
        _mouseDeltaInput = Mouse.current.delta.ReadValue();
        _mousePosition = Mouse.current.position.value;
    }

    private void CalculateInput()
    {
        if (_mouseDeltaInput == Vector2.zero)
            InputDeltaResult = _touchDeltaInput;
        else
            InputDeltaResult = _mouseDeltaInput;

        if (_touchPosition != Vector2.zero)
            InputPosition = _touchPosition;
        else if (_mousePosition != Vector2.zero)
            InputPosition = _mousePosition;
    }

    public void Dispose()
    {
        _control.Map.LeftMouseButton.performed -= LeftMouseButton_performed;
        _control.Map.LeftMouseButton.canceled -= LeftMouseButton_canceled;
    }
}