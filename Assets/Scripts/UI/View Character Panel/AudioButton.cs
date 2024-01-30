using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private Button _iconButton;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Sprite _pauseSprite;
    [SerializeField] private Sprite _playSprite;
    [SerializeField] private Text _mediaLength;

    private AudioClip _audioClip;
    private AudioPlayerService _audioPlayerService;

    public void Init()
    {
        _iconButton.onClick.AddListener(() => OnButtonClick());
        _audioPlayerService = ServiceLocator.Instance.AudioPlayerService;
        _audioPlayerService.AudioEnd += OnAudioEnd;
    }

    public void SetContent(AudioClip audioClip)
    {
        _audioClip = audioClip;
        ShowInactiveState();
    }

    private void OnButtonClick()
    {
        if (!_audioPlayerService.IsPlaying)
        {
            _audioPlayerService.Play(_audioClip);
            ShowActiveState();
        }
        else
        {
            _audioPlayerService.Stop();
            ShowInactiveState();
        }
    }

    private void ShowInactiveState()
    {
        _iconImage.sprite = _pauseSprite;
        _mediaLength.text = TimeSpan.FromSeconds(_audioClip.length).ToString("mm\\:ss");
    }

    private void ShowActiveState()
    {
        _iconImage.sprite = _playSprite;
    }

    private void OnEnable()
    {
        if (_audioPlayerService != null)
            _audioPlayerService.AudioEnd += OnAudioEnd;
    }

    private void OnDisable()
    {
        if (_audioPlayerService != null)
            _audioPlayerService.AudioEnd -= OnAudioEnd;
    }

    private void OnAudioEnd() =>
        ShowInactiveState();

    private void Update()
    {
        if (_audioPlayerService.IsPlaying)
            _mediaLength.text = $"{_audioPlayerService.MediaCurrentTime}/{_audioPlayerService.MediaLength}";
    }
}