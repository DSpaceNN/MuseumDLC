using UnityEngine;
using UnityEngine.UI;

public class StartPanelCharacterIcon : CharacterIconBase
{
    [SerializeField] private GameObject _redHaloGo;
    [SerializeField] private GameObject _chooseIcon;
    [SerializeField] private GameObject _choosenIcon;
    [SerializeField] private Button _iconButton;
    [SerializeField] private Image _characterIcon;

    private CharactersStorage _characterStorage;

    public override void Init(string characterId)
    {
        base.Init(characterId);

        _characterStorage = ServiceLocator.Instance.CharactersStorage;
        _iconButton.onClick.AddListener(() => { OnStartCharacterIconClick?.Invoke(CharacterId); });
        _characterIcon.sprite = _characterStorage.GetCharacterById(CharacterId).CharacterSprite;
    }

    protected override void ShowChoosenState()
    {
        _redHaloGo.SetActive(true);
        _chooseIcon.SetActive(false);
        _choosenIcon.SetActive(true);
    }

    protected override void ShowUnchoosenState()
    {
        _redHaloGo.SetActive(false);
        _chooseIcon.SetActive(true);
        _choosenIcon.SetActive(false);
    }
}
