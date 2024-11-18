using UnityEngine;
using UnityEngine.UI;

public class ViewCharacterPanelBase : PanelBase, IMainPanel
{
    [SerializeField] protected RawImage _mainCharacterImage;
    [SerializeField] protected MainCharacterRawImageBehaviour _mainCharacterIcon;
    [SerializeField] protected ItemsPanelBase _itemPanelBase;
    [SerializeField] protected ViewItemInfoPanel _viewItemInfoPanel;
    [SerializeField] protected ViewCharacterInfoPanel _characterInfoPanel;
    [SerializeField] protected WinCharacterInfoPanel _winCharacterInfoPanel;

    [SerializeField] protected Button _resetButton;

    protected CharacterChanger _characterChanger;
    protected AudioPlayerService _audioPlayerService;

    public override void Initialize(CanvasController canvasController, PanelsController panelsController, IPanelBaseData dataForOpen = null)
    {
        base.Initialize(canvasController, panelsController, dataForOpen);

        OnInitialize();

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

    public override void CleanUpPanel()
    {
        _characterChanger.ShowNewCharacter -= OnChangeCharacter;
        ItemIcon.OnClickOnItem -= ItemIcon_OnClickOnItem;
        CharacterDresser.CharacterIsFullyEquiped -= OnCharacterFullyEquipped;

        OnCleanup();
    }

    protected void OnCharacterFullyEquipped() =>
        ShowCharacterWinPanel();

    protected virtual void OnChangeCharacter(CharacterSo characterSo)
    {
        ShowCharacterInfoPanel();
        _itemPanelBase.ShowIcons(characterSo);
    }

    public virtual void ShowCharacterInfoPanel()
    {
        _viewItemInfoPanel.gameObject.SetActive(false);
        _characterInfoPanel.gameObject.SetActive(true);
        _winCharacterInfoPanel.gameObject.SetActive(false);

        _characterInfoPanel.OnShowCharacter(_characterChanger.CurrentCharacter);
        _audioPlayerService.Stop();
    }

    protected virtual void ItemIcon_OnClickOnItem(CharacterItemSo obj)
    {
        ShowItemInfoPanel();
        _viewItemInfoPanel.OnClickOnItem(obj);
    }

    public virtual void ShowItemInfoPanel()
    {
        _viewItemInfoPanel.gameObject.SetActive(true);
        _characterInfoPanel.gameObject.SetActive(false);
        _winCharacterInfoPanel.gameObject.SetActive(false);
        _audioPlayerService.Stop();
    }

    public virtual void ShowCharacterWinPanel()
    {
        //_itemsPanel.ActivateInfoButton();                 //это в тёмную панель переезжает
        _viewItemInfoPanel.gameObject.SetActive(false);
        _characterInfoPanel.gameObject.SetActive(false);
        _winCharacterInfoPanel.gameObject.SetActive(true);
        _audioPlayerService.Stop();
    }

    protected virtual void OnCleanup()
    {

    }

    protected virtual void OnInitialize()
    {

    }

    protected virtual void ResetButton()
    {
        if (ServiceLocator.Instance.CharacterDresser.CharacterDressCounter == 0)
            return;

        CanvasController.ShowPanelById(PanelsIdHolder.ResetGamePopupPanelId);
    }
}

public class ViewCharacterDarkPanel : ViewCharacterPanelBase
{
    [SerializeField] private TopButtonsPanel _topButtonsPanel;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        _topButtonsPanel.Init(CanvasController);
    }

    protected override void OnCleanup() 
    {
        base.OnCleanup();

    }
}

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

    protected override void OnCleanup()
    {
        base.OnCleanup();

    }
}