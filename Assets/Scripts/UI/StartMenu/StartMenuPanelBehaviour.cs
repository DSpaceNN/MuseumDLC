using UnityEngine;
using UnityEngine.UI;

public class StartMenuPanelBehaviour : PanelBase, IMainPanel
{
    public string StartCharacterId { get; private set; }

    [SerializeField] private string _startCharacterId;
    [SerializeField] private Dropdown _settingsDropDown;
    [SerializeField] private StartPanelCharacterIcon[] _characterIcons;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _startButton;

    public override void Initialize(CanvasController canvasController, PanelsController panelsController, IPanelBaseData dataForOpen = null)
    {
        base.Initialize(canvasController, panelsController, dataForOpen);
        StartPanelCharacterIcon.OnStartCharacterIconClick += OnStartCharacterIconClick;
        StartCharacterId = _startCharacterId;

        InitButtons();
    }

    public override void CleanUpPanel() =>
        StartPanelCharacterIcon.OnStartCharacterIconClick -= OnStartCharacterIconClick;

    private void InitButtons()
    {
        foreach (var icon in _characterIcons)
            icon.Init(this);

        _exitButton.onClick.AddListener(() => ExitButton());
        _startButton.onClick.AddListener(() => StartButton());
    }

    private void OnStartCharacterIconClick(string characterId) =>
        StartCharacterId = characterId;

    private void ExitButton() =>
        CanvasController.ShowPanelById(PanelsIdHolder.ExitPopupPanellId);

    private void StartButton()
    {
        ServiceLocator.Instance.CharacterChanger.ShowCharacterById(StartCharacterId);
        CanvasController.ShowPanelById(PanelsIdHolder.ViewCharacterPanelId);
    }
}