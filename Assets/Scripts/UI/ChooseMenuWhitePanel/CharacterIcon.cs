using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIcon : MonoBehaviour
{
    public static event Action<string> OnIconClick;

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

    //TODO прокинуть SO персонажа сюда
    public void Init(string characterId)
    {
        _iconButton.onClick.AddListener(() => OnCharacterIconClick());
        CharacterIcon.OnIconClick += CharacterIcon_OnIconClick;
    }

    public void SetChoosenState()
    {

    }

    private void CharacterIcon_OnIconClick(string characterId)
    {
        
    }

    private void OnCharacterIconClick() =>
        OnIconClick?.Invoke(_currentCharacterId);

    private void ShowChoosenState()
    {
        _fadeDarkImage.gameObject.SetActive(true);
        _frameImage.color = _darkFrameColor;
    }

    private void ShowUnchoosenState()
    {
        _fadeDarkImage.gameObject.SetActive(false);
        _frameImage.color= _lightFrameColor;
    }

    private void OnDestroy()
    {
        CharacterIcon.OnIconClick -= CharacterIcon_OnIconClick;
    }
}