using UnityEngine;
using UnityEngine.UI;

public class ViewCharacterWhitePanel : ViewCharacterPanelBase
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _tasksButton;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        _backButton.onClick.AddListener(() => OnBackButtonClick());
        _tasksButton.onClick.AddListener(() => OnTasksButtonClick());
    }

    private void OnBackButtonClick() =>
        CanvasController.ShowPanelById(PanelsIdHolder.ChooseMenuWhitePanelId);

    private void OnTasksButtonClick()
    {
        //TODO переименовать, смысл уже потерялся
        ShowCharacterWinPanel();
    }

    protected override void ResetButton()
    {
        base.ResetButton();

        if (ServiceLocator.Instance.CharacterDresser.CharacterDressCounter == 0)
            return;

        CanvasController.ShowPanelById(PanelsIdHolder.ResetGameWhitePopupPanelId);
    }

    protected override void OnCleanup()
    {
        base.OnCleanup();
    }
}