using UnityEngine;
using UnityEngine.UI;

public class CharacterIcon : CharacterIconBase
{
    [Header("Colors for Frame")]
    [SerializeField] private Color _lightFrameColor;
    [SerializeField] private Color _darkFrameColor;

    [Space]
    [Header("Dependencies")]
    [SerializeField] private Button _iconButton;
    [SerializeField] private Image _frameImage;
    [SerializeField] private Image _fadeDarkImage;
    [SerializeField] private Image _characterImage;
    [SerializeField] private Text _characterNameText;

    private string _currentCharacterId;
    private CharacterSo _currentCharacterSo;

    public override void Init(string characterId)
    {
        base.Init(characterId);

        _currentCharacterSo = ServiceLocator.Instance.CharactersStorage.GetCharacterById(characterId);

        if (_currentCharacterSo.CharacterFullLength != null)
            _characterImage.sprite = _currentCharacterSo.CharacterFullLength;

        _characterNameText.text = _currentCharacterSo.CharacterName;

        _iconButton.onClick.AddListener(() => { OnStartCharacterIconClick?.Invoke(CharacterId); });
    }

    protected override void ShowChoosenState()
    {
        _fadeDarkImage.gameObject.SetActive(true);
        _frameImage.color = _darkFrameColor;
    }

    protected override void ShowUnchoosenState()
    {
        _fadeDarkImage.gameObject.SetActive(false);
        _frameImage.color = _lightFrameColor;
    }
}