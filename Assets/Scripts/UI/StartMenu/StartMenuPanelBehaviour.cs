using System.Linq;
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

    private CharactersStorage _characterStorage;

    public override void Initialize(CanvasController canvasController, PanelsController panelsController, IPanelBaseData dataForOpen = null)
    {
        base.Initialize(canvasController, panelsController, dataForOpen);
        StartPanelCharacterIcon.OnStartCharacterIconClick += OnStartCharacterIconClick;
        StartCharacterId = _startCharacterId;
        _characterStorage = ServiceLocator.Instance.CharactersStorage;

        InitButtons();
    }

    public override void CleanUpPanel() =>
        StartPanelCharacterIcon.OnStartCharacterIconClick -= OnStartCharacterIconClick;

    private void InitButtons()
    {
        for (int i = 0; i < _characterIcons.Length; i++)
            _characterIcons[i].Init(_characterStorage.Characters[i].Id);

        StartPanelCharacterIcon choosenIcon = _characterIcons.FirstOrDefault(x => x.CharacterId == _startCharacterId);
        choosenIcon.SetChoosenState();

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

        string dropDownValue = _settingsDropDown.options[_settingsDropDown.value].text;
        string[] temp = dropDownValue.Split(" ");
        dropDownValue = temp[0];

        float durationValue;

        if (int.TryParse(dropDownValue, out int duration))
            durationValue = (float)duration;
        else
            durationValue = float.PositiveInfinity;

        ServiceLocator.Instance.InitDefaultPanelSwitcher(durationValue, StartCharacterId);
    }
}