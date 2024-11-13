using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIcon : MonoBehaviour
{
    public static event Action OnIconClick;

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

    //TODO прокинуть SO персонажа сюда
    public void Init()
    {
        _iconButton.onClick.AddListener(() => OnCharacterIconClick());
        CharacterIcon.OnIconClick += CharacterIcon_OnIconClick;
    }

    private void CharacterIcon_OnIconClick()
    {
        
    }

    private void OnCharacterIconClick() =>
        OnIconClick?.Invoke();

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