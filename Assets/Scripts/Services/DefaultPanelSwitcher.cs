using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class DefaultPanelSwitcher
{
    public DefaultPanelSwitcher(CanvasController canvasController, CharacterChanger characterChanger)
    {
        _canvasControllerMb = canvasController;
        _characterChanger = characterChanger;
    }

    private CanvasController _canvasControllerMb;
    private CharacterChanger _characterChanger;
    private float _durationTime;
    private string _defaultCharacter;

    private float _timer;
    private bool _isWatching;

    public void Init(float durationTime, string defaultCharacter)
    {
        if (durationTime != float.PositiveInfinity)
            _durationTime = durationTime * 60f;
        else
            _durationTime = float.PositiveInfinity;

        _defaultCharacter = defaultCharacter;
        _timer = 0;
        _isWatching = true;
    }

    public void StartWatching()
    {
        _isWatching = true;
        _timer = 0;
    }

    public void StopWatching()
    {
        _isWatching = false;
    }

    public void Update()
    {
        if (_isWatching)
        {
            HandleTimer();
            HandleTouch();
        }
    }

    private void HandleTimer()
    {
        _timer += Time.deltaTime;
        if (_timer > _durationTime)
            SwitchToDefault();
    }

    private void HandleTouch()
    {
        if (Touch.activeTouches.Count >= 1 || Input.GetMouseButtonDown(0))
            _timer = 0;
    }

    private void SwitchToDefault()
    {
        Debug.Log("SwitchToDefault()");
        _timer = 0;
        _characterChanger.ShowCharacterById(_defaultCharacter);
    }
}