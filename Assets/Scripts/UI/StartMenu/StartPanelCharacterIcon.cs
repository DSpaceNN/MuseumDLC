using System;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelCharacterIcon : MonoBehaviour
{
    [SerializeField] private string _characterId;
    [SerializeField] private GameObject _redHaloGo;
    [SerializeField] private GameObject _chooseIcon;
    [SerializeField] private GameObject _choosenIcon;
    [SerializeField] private Button _iconButton;

    private StartMenuPanelBehaviour _startMenuMb;

    public static Action<string> OnStartCharacterIconClick;

    public void Init(StartMenuPanelBehaviour startMenuBehaviour)
    {
        _startMenuMb = startMenuBehaviour;

        _iconButton.onClick.AddListener(() => { OnStartCharacterIconClick?.Invoke(_characterId); });
        StartPanelCharacterIcon.OnStartCharacterIconClick += OnCharcterIconClick;

        OnCharcterIconClick(startMenuBehaviour.StartCharacterId);
    }

    private void OnCharcterIconClick(string characterId)
    {
        if (_characterId == characterId)
            ShowChoosenIcon();
        else
            ShowChooseIcon();
    }

    public void ShowChooseIcon()
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
        StartPanelCharacterIcon.OnStartCharacterIconClick -= OnCharcterIconClick;
}