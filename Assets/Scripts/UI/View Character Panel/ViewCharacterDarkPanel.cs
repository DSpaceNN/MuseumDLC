using UnityEngine;

public class ViewCharacterDarkPanel : ViewCharacterPanelBase
{
    [SerializeField] private TopButtonsPanel _topButtonsPanel;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        _topButtonsPanel.Init(CanvasController);
        ItemsPanelBase.OnInfoButtonClick += ItemsPanelBase_OnInfoButtonClick;
        ItemsPanelBase.OnQuestionButtonClick += ItemsPanelBase_OnQuestionButtonClick;
    }

    private void ItemsPanelBase_OnQuestionButtonClick() =>
        ShowCharacterInfoPanel();

    private void ItemsPanelBase_OnInfoButtonClick() =>
        ShowCharacterWinPanel();

    public override void ShowCharacterWinPanel()
    {
        base.ShowCharacterWinPanel();

        //TODO порефакторить
        var itemPanel = _itemPanelBase as ItemsPanelBehaviour;
        itemPanel.ActivateInfoButton();
    }

    protected override void ResetButton()
    {
        base.ResetButton();

        if (ServiceLocator.Instance.CharacterDresser.CharacterDressCounter == 0)
            return;

        CanvasController.ShowPanelById(PanelsIdHolder.ResetGamePopupPanelId);
    }

    protected override void OnCleanup() 
    {
        base.OnCleanup();
        ItemsPanelBase.OnInfoButtonClick += ItemsPanelBase_OnInfoButtonClick;
        ItemsPanelBase.OnQuestionButtonClick += ItemsPanelBase_OnQuestionButtonClick;
    }
}
