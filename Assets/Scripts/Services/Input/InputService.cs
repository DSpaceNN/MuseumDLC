using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputService
{
    public InputService() 
    {
        EnhancedTouchSupport.Enable();
    }

    public Vector2 MouseInput { get; private set; }
    public Vector2 TouchInput { get; private set; }
    public Vector2 InputResult { get; private set; }

    public Vector2 StartTouchPosition { get; private set; }

    //private bool _isListening;

    public void SetLimitationFrame()
    {

    }

    //public void StartListen() =>
    //    _isListening = true;

    //public void StopListen() => 
    //    _isListening = false;

    public void Update() 
    {
        HandleTouchInput();
        HandleMouseInput();
        CalculateInput();
    }

    private void HandleTouchInput()
    {
        if (Touch.activeTouches.Count >= 1 && Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Began)
            StartTouchPosition = Touch.activeTouches[0].screenPosition;

        if (Touch.activeTouches.Count >= 1 && Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Moved)
            TouchInput = Touch.activeTouches[0].delta;
        else
            TouchInput = Vector2.zero;
    }

    private void HandleMouseInput()
    {
        MouseInput = Mouse.current.delta.ReadValue();
    }

    private void CalculateInput()
    {
        if (MouseInput == Vector2.zero)
            InputResult = TouchInput;
        else
            InputResult = MouseInput;
    }
}