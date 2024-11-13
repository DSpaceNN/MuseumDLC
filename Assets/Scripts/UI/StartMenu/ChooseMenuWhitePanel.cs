using UnityEngine;
using UnityEngine.UI;

public class ChooseMenuWhitePanel : PanelBase, IMainPanel
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;

    public override void Initialize(CanvasController canvasController, PanelsController panelsController, IPanelBaseData dataForOpen = null)
    {
        base.Initialize(canvasController, panelsController, dataForOpen);

        _startButton.onClick.AddListener(() => OnStartButtonClick());
        _quitButton.onClick.AddListener(() => OnQuitButtonClick());
    }

    private void OnStartButtonClick()
    {

    }

    private void OnQuitButtonClick()
    {

    }
}