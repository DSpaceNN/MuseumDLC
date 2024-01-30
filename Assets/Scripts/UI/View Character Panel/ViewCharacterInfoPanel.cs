using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewCharacterInfoPanel : MonoBehaviour
{
    [SerializeField] private AudioButton _audioButton;
    [SerializeField] private Text _audioLength;
    [SerializeField] private Text _taskText;
    [SerializeField] private Text _purposeText;

    private CharacterChanger _characterChanger;
    private CharacterSo _currentCharacter;
    private AudioPlayerService _audioPlayer;

    public void Init()
    {
        _characterChanger = ServiceLocator.Instance.CharacterChanger;
        _audioPlayer = ServiceLocator.Instance.AudioPlayerService;

        _characterChanger.ShowNewCharacter += OnShowCharacter;
        OnShowCharacter(_characterChanger.CurrentCharacter);

        _audioButton.Init();
        _audioButton.SetContent(_currentCharacter.CharacterTaskAudioClip);
    }

    public void OnShowCharacter(CharacterSo characterSo)
    {
        _currentCharacter = characterSo;
        _taskText.text = _currentCharacter.Task;
        _purposeText.text = _currentCharacter.Purpose;
        _audioButton.SetContent(characterSo.CharacterTaskAudioClip);
    }
}