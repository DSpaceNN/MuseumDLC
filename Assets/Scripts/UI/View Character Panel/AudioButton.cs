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

    [SerializeField] private AudioClip _audioClip;
    private AudioPlayerService _audioPlayerService;
    private bool _isFirstClick;

    public void Init()
    {
        _iconButton.onClick.AddListener(() => OnButtonClick());
        _audioPlayerService = ServiceLocator.Instance.AudioPlayerService;
        _audioPlayerService.AudioEnd += OnAudioEnd;
    }

    public void SetContent(AudioClip audioClip)
    {
        Debug.Log("SetContent");
        if (audioClip == null)
            Debug.Log("SetContent audioClip == null");

        _isFirstClick = true;
        _audioClip = audioClip;
        ShowInactiveState();
    }

    private void OnButtonClick()
    {
        if (!_audioPlayerService.IsPlaying)
        {
            if(_isFirstClick)
            {
                _audioPlayerService.Play(_audioClip);
                ShowActiveState();
                _isFirstClick = false;
            }
            else
            {
                _audioPlayerService.PlayAfterPause();
                ShowActiveState();
            }
        }
        else
        {
            _audioPlayerService.Pause();
            ShowPauseState();
        }
    }

    private void ShowInactiveState()
    {
        _iconImage.sprite = _pauseSprite;

        Debug.Log("ShowInactiveState()");

        if (_audioClip == null)
        {
            Debug.Log("_audioClip == null");
            Transform parent = this.transform.parent;
            Transform grandParent = parent.parent;
            Debug.Log($"parent = {parent.name}; grandParent = {grandParent.name};");
        }

        _mediaLength.text = TimeSpan.FromSeconds(_audioClip.length).ToString("mm\\:ss");
    }

    private void ShowActiveState() =>
        _iconImage.sprite = _playSprite;
    
    private void ShowPauseState() =>
        _iconImage.sprite = _pauseSprite;

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