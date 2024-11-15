using UnityEngine;
using UnityEngine.UI;

public class ContinueTimesupPopupPanel : PanelBase, IPopupPanel
{
    [SerializeField] private Button _approveButton;
    [SerializeField] private Text _timerText;
    [SerializeField] private float _timerDuration = 5.9f;

    private float _timer;

    public override void Initialize(CanvasController canvasController, PanelsController panelsController, IPanelBaseData dataForOpen = null)
    {
        base.Initialize(canvasController, panelsController, dataForOpen);

        _timer = _timerDuration;
        _approveButton.onClick.AddListener(() => OnApproveButtonClick());
    }

    private void OnApproveButtonClick()
    {

    }

    private void OnTimesup()
    {

    }

    private void Update()
    {
        HandleTimer();
    }

    private void HandleTimer()
    {
        _timer -= Time.deltaTime;
        _timer = Mathf.Clamp(_timer, 0f, _timerDuration);
        //TODO переделать по человечески
        _timerText.text = $"00:0{Mathf.FloorToInt(_timer)}";

        if (_timer == 0)
            OnTimesup();
    }
}