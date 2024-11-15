using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMenuWhitePanel : PanelBase, IMainPanel
{
    public string StartCharacterId { get; private set; }

    [SerializeField] private string _startCharacterId = "Soldier";
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private List<CharacterIcon> _characterIcons;

    private ServiceLocator _serviceLocator;
    private CharactersStorage _characterStorage;

    public override void Initialize(CanvasController canvasController, PanelsController panelsController, IPanelBaseData dataForOpen = null)
    {
        base.Initialize(canvasController, panelsController, dataForOpen);

        CharacterIconBase.OnStartCharacterIconClick += OnStartCharacterIconClick;
        StartCharacterId = _startCharacterId;
        _serviceLocator = ServiceLocator.Instance;
        _characterStorage = _serviceLocator.CharactersStorage;

        InitButtons();
    }

    private void InitButtons()
    {
        for (int i = 0; i < _characterIcons.Count; i++)
            _characterIcons[i].Init(_characterStorage.Characters[i].Id);

        CharacterIcon choosenIcon = _characterIcons.FirstOrDefault(x => x.CharacterId == _startCharacterId);
        choosenIcon.SetChoosenState();

        _startButton.onClick.AddListener(() => OnStartButtonClick());
        _quitButton.onClick.AddListener(() => OnQuitButtonClick());
    }

    private void OnStartCharacterIconClick(string characterId) =>
        StartCharacterId = characterId;

    private void OnStartButtonClick()
    {
        _serviceLocator.CharacterChanger.ShowCharacterById(StartCharacterId);
        CanvasController.ShowPanelById(PanelsIdHolder.ViewCharacterWhitePanelId);

        //TODO этим ещё нужно будет заняться
        _serviceLocator.InitDefaultPanelSwitcher(60f, StartCharacterId);
    }

    private void OnQuitButtonClick() =>
        CanvasController.ShowPanelById(PanelsIdHolder.ExitPopupPanellId);

    public override void CleanUpPanel() =>
        CharacterIconBase.OnStartCharacterIconClick -= OnStartCharacterIconClick;
}