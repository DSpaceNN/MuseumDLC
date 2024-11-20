using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    public AudioClip AudioClip { get; private set; }

    [SerializeField] private Button _iconButton;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Sprite _pauseSprite;
    [SerializeField] private Sprite _playSprite;
    [SerializeField] private Text _mediaLength;

    private AudioPlayerService _audioPlayerService;

    public void Init()
    {
        _iconButton.onClick.AddListener(() => OnButtonClick());
        _audioPlayerService = ServiceLocator.Instance.AudioPlayerService;
    }

    public void SetContent(AudioClip audioClip)
    {
        AudioClip = audioClip;
        ShowInactiveState();
    }

    private void OnButtonClick() =>
        _audioPlayerService.WorkWithAudioButton(this);

    public void ShowInactiveState()
    {
        _iconImage.sprite = _pauseSprite;
        _mediaLength.text = TimeSpan.FromSeconds(AudioClip.length).ToString("mm\\:ss");
    }
    
    public void ShowTextInPlay(string mediaCurrentTime, string mediaLength) =>
        _mediaLength.text = $"{mediaCurrentTime}/{mediaLength}";

    public void ShowActiveState() =>
        _iconImage.sprite = _playSprite;
    
    public void ShowPauseState() =>
        _iconImage.sprite = _pauseSprite;
}