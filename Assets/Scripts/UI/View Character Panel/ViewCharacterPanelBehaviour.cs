using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewCharacterPanelBehaviour : PanelBase, IMainPanel
{
    [SerializeField] private RawImage _mainCharacterImage;
    [SerializeField] private MainCharacterRawImageBehaviour _mainCharacterIcon;
    [SerializeField] private ItemsPanelBehaviour _itemsPanel;
    [SerializeField] private TopButtonsPanel _topButtonsPanel;
    [SerializeField] private ViewItemInfoPanel _viewItemInfoPanel;
    [SerializeField] private ViewCharacterInfoPanel _characterInfoPanel;
    [SerializeField] private Button _resetButton;

    private CharacterChanger _characterChanger;

    public override void Initialize(CanvasController canvasController, PanelsController panelsController, IPanelBaseData dataForOpen = null)
    {
        base.Initialize(canvasController, panelsController, dataForOpen);

        _itemsPanel.Init(canvasController);
        _topButtonsPanel.Init(canvasController);

        _characterChanger = ServiceLocator.Instance.CharacterChanger;

        _characterChanger.ShowNewCharacter += OnChangeCharacter;
        ItemIcon.OnClickOnItem += ItemIcon_OnClickOnItem;
        _resetButton.onClick.AddListener(() => ResetButton());

        OnChangeCharacter(_characterChanger.CurrentCharacter);

        ShowCharacterInfoPanel();
    }

    private void ItemIcon_OnClickOnItem(CharacterItemSo obj)
    {
        ShowItemInfoPanel();
    }

    private void OnChangeCharacter(CharacterSo characterSo)
    {
        _itemsPanel.ShowIcons(characterSo);
        ShowCharacterInfoPanel();
    }

    public override void CleanUpPanel()
    {
        _characterChanger.ShowNewCharacter -= OnChangeCharacter;
        ItemIcon.OnClickOnItem -= ItemIcon_OnClickOnItem;
    }

    private void ShowItemInfoPanel()
    {
        _viewItemInfoPanel.gameObject.SetActive(true);
        _characterInfoPanel.gameObject.SetActive(false);
    }

    private void ShowCharacterInfoPanel()
    {
        _viewItemInfoPanel.gameObject.SetActive(false);
        _characterInfoPanel.gameObject.SetActive(true);
    }

    private void ResetButton()
    {
        if (ServiceLocator.Instance.CharacterDresser.CharacterDressCounter == 0)
            return;

        CanvasController.ShowPanelById(PanelsIdHolder.ResetGamePopupPanelId);
    }
}