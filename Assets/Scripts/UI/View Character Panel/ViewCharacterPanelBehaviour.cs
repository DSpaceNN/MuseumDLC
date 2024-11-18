using UnityEngine;
using UnityEngine.UI;

public class ViewCharacterPanelBehaviour : PanelBase, IMainPanel
{
    [SerializeField] private RawImage _mainCharacterImage;
    [SerializeField] private MainCharacterRawImageBehaviour _mainCharacterIcon;
    [SerializeField] private ItemsPanelBehaviour _itemsPanel;
    [SerializeField] private ItemsWhitePanelBehaviour _itemsWhitePanel;
    [SerializeField] private TopButtonsPanel _topButtonsPanel;
    [SerializeField] private ViewItemInfoPanel _viewItemInfoPanel;
    [SerializeField] private ViewCharacterInfoPanel _characterInfoPanel;
    [SerializeField] private WinCharacterInfoPanel _winCharacterInfoPanel;
    [SerializeField] private Button _resetButton;

    //TODO переделать потом на абстрактный класс, если такого много наберется
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _tasksButton;

    private CharacterChanger _characterChanger;
    private AudioPlayerService _audioPlayerService;

    public override void Initialize(CanvasController canvasController, PanelsController panelsController, IPanelBaseData dataForOpen = null)
    {
        base.Initialize(canvasController, panelsController, dataForOpen);

        switch (ServiceLocator.Instance.InterfaceType)
        {
            case Enums.InterfaceType.DarkTheme:
                _itemsPanel.Init(canvasController, this);
                _topButtonsPanel.Init(canvasController);
                break;

            case Enums.InterfaceType.WhiteTheme:
                _itemsWhitePanel.Init(canvasController);
                _backButton.onClick.AddListener(() => OnBackButtonClick());
                _tasksButton.onClick.AddListener(() => OnTasksButtonClick());    
                break;
        }

        _characterInfoPanel.Init();
        _viewItemInfoPanel.Init();
        _mainCharacterIcon.Init();
        _winCharacterInfoPanel.Init();

        _characterChanger = ServiceLocator.Instance.CharacterChanger;
        _audioPlayerService = ServiceLocator.Instance.AudioPlayerService;

        _characterChanger.ShowNewCharacter += OnChangeCharacter;
        ItemIcon.OnClickOnItem += ItemIcon_OnClickOnItem;
        CharacterDresser.CharacterIsFullyEquiped += OnCharacterFullyEquipped;

        _resetButton.onClick.AddListener(() => ResetButton());

        OnChangeCharacter(_characterChanger.CurrentCharacter);

        ShowCharacterInfoPanel();
    }

    private void OnBackButtonClick() =>
        CanvasController.ShowPanelById(PanelsIdHolder.ChooseMenuWhitePanelId);

    private void OnTasksButtonClick()
    {
        //TODO переименовать, смысл уже потерялся
        ShowCharacterWinPanel();
    }

    private void OnCharacterFullyEquipped() =>
        ShowCharacterWinPanel();

    public void ShowCharacterInfoPanel()
    {
        _viewItemInfoPanel.gameObject.SetActive(false);
        _characterInfoPanel.gameObject.SetActive(true);
        _winCharacterInfoPanel.gameObject.SetActive(false);

        _characterInfoPanel.OnShowCharacter(_characterChanger.CurrentCharacter);
        _audioPlayerService.Stop();
    }

    public void ShowItemInfoPanel()
    {
        _viewItemInfoPanel.gameObject.SetActive(true);
        _characterInfoPanel.gameObject.SetActive(false);
        _winCharacterInfoPanel.gameObject.SetActive(false);
        _audioPlayerService.Stop();
    }

    public void ShowCharacterWinPanel()
    {
        _itemsPanel.ActivateInfoButton();
        _viewItemInfoPanel.gameObject.SetActive(false);
        _characterInfoPanel.gameObject.SetActive(false);
        _winCharacterInfoPanel.gameObject.SetActive(true);
        _audioPlayerService.Stop();
    }

    private void ItemIcon_OnClickOnItem(CharacterItemSo obj)
    {
        ShowItemInfoPanel();
        _viewItemInfoPanel.OnClickOnItem(obj);
    }

    private void OnChangeCharacter(CharacterSo characterSo)
    {
        switch (ServiceLocator.Instance.InterfaceType)
        {
            case Enums.InterfaceType.DarkTheme:
                _itemsPanel.ShowIcons(characterSo);
                break;
            case Enums.InterfaceType.WhiteTheme:
                _itemsWhitePanel.ShowIcons(characterSo);
                break;
        }   

        ShowCharacterInfoPanel();
    }

    public override void CleanUpPanel()
    {
        _characterChanger.ShowNewCharacter -= OnChangeCharacter;
        ItemIcon.OnClickOnItem -= ItemIcon_OnClickOnItem;
        CharacterDresser.CharacterIsFullyEquiped -= OnCharacterFullyEquipped;
    }

    private void ResetButton()
    {
        if (ServiceLocator.Instance.CharacterDresser.CharacterDressCounter == 0)
            return;

        CanvasController.ShowPanelById(PanelsIdHolder.ResetGamePopupPanelId);
    }
}