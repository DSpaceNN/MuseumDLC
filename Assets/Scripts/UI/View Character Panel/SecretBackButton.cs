using UnityEngine;
using UnityEngine.EventSystems;

public class SecretBackButton : MonoBehaviour, IPointerDownHandler
{
    private CanvasController _canvasController;

    private int _tapCountToExit = 3;
    private int _tapCounter;

    private float _timeToTapDuration = 1f;
    private float _timer;

    private void Start()
    {
        _canvasController = ServiceLocator.Instance.CanvasController;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_timer <= 0)
        {
            _timer = _timeToTapDuration;
            _tapCounter++;
        }
        else
        {
            _tapCounter++;
            if (_tapCounter >= _tapCountToExit)
            {
                _tapCounter = 0;
                OpenAdminPanel();
            }
        }
    }

    private void OpenAdminPanel() =>
        _canvasController.ShowStartPanel();

    private void Update()
    {
        if (_timer >= 0)
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
                _tapCounter = 0;
        }
    }
}
