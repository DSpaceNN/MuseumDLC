using System;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelCharacterIcon : MonoBehaviour
{
    public static Action<string> OnStartCharacterIconClick;
    public string CharacterId { get; private set; }

    [SerializeField] private GameObject _redHaloGo;
    [SerializeField] private GameObject _chooseIcon;
    [SerializeField] private GameObject _choosenIcon;
    [SerializeField] private Button _iconButton;
    [SerializeField] private Image _characterIcon;

    private CharactersStorage _characterStorage;

    public void Init(string characterId)
    {
        _characterStorage = ServiceLocator.Instance.CharactersStorage;
        CharacterId = characterId;

        _iconButton.onClick.AddListener(() => { OnStartCharacterIconClick?.Invoke(CharacterId); });
        StartPanelCharacterIcon.OnStartCharacterIconClick += OnCharacterIconClick;
        _characterIcon.sprite = _characterStorage.GetCharacterById(CharacterId).CharacterSprite;
    }

    public void SetChoosenState() =>
        OnStartCharacterIconClick?.Invoke(CharacterId);

    private void OnCharacterIconClick(string characterId)
    {
        if (CharacterId == characterId)
            ShowChoosenIcon();
        else
            ShowUnchoosenIcon();
    }

    private void ShowUnchoosenIcon()
    {
        _redHaloGo.SetActive(false);
        _chooseIcon.SetActive(true);
        _choosenIcon.SetActive(false);
    }

    public void ShowChoosenIcon()
    {
        _redHaloGo.SetActive(true);
        _chooseIcon.SetActive(false);
        _choosenIcon.SetActive(true);
    }

    private void OnDestroy() =>
        StartPanelCharacterIcon.OnStartCharacterIconClick -= OnCharacterIconClick;
}

public class CharacterIconBase : MonoBehaviour
{
    public static Action<string> OnStartCharacterIconClick;
    public string CharacterId { get; private set; }

    public virtual void Init(string characterId)
    {
        CharacterId = characterId;
        CharacterIconBase.OnStartCharacterIconClick += OnCharacterIconClick;
    }

    public virtual void SetChoosenState() =>
        OnStartCharacterIconClick?.Invoke(CharacterId);

    protected virtual void OnCharacterIconClick(string characterId)
    {
        if (characterId == CharacterId)
            ShowChoosenState();
        else
            ShowUnchoosenState();
    }

    protected virtual void ShowChoosenState() { }
    protected virtual void ShowUnchoosenState() { }

    protected virtual void OnDestroy()
    {
        CharacterIconBase.OnStartCharacterIconClick -= OnCharacterIconClick;
    }
}