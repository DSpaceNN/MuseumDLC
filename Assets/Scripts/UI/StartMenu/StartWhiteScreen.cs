using UnityEngine;
using UnityEngine.UI;

public class StartWhiteScreen : PanelBase, IMainPanel
{
    [SerializeField] private Button _startButton;   
    [SerializeField] private Button _quitButton;
    [SerializeField] private GameObject _videoImage;

    public override void Initialize(CanvasController canvasController, PanelsController panelsController, IPanelBaseData dataForOpen = null)
    {
        base.Initialize(canvasController, panelsController, dataForOpen);

        _videoImage.SetActive(true);

        _startButton.onClick.AddListener(() => OnStartButtonClick());
        _quitButton.onClick.AddListener(() => OnQuitButtonClick());
    }

    private void OnStartButtonClick() =>
        CanvasController.ShowPanelById(PanelsIdHolder.ChooseMenuWhitePanelId);

    private void OnQuitButtonClick() =>
        CanvasController.ShowPanelById(PanelsIdHolder.ExitWhitePopupPanellId);
}