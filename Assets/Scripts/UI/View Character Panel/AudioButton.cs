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
    private bool _isFirstClick;

    public void Init()
    {
        _iconButton.onClick.AddListener(() => OnButtonClick());
        _audioPlayerService = ServiceLocator.Instance.AudioPlayerService;
    }

    public void SetContent(AudioClip audioClip)
    {
        Debug.Log("SetContent");
        if (audioClip == null)
            Debug.Log("SetContent audioClip == null");

        _isFirstClick = true;
        AudioClip = audioClip;
        ShowInactiveState();
    }

    private void OnButtonClick()
    {
        _audioPlayerService.WorkWithAudioButton(this);

        //if (!_audioPlayerService.IsPlaying)
        //{
        //    if(_isFirstClick)
        //    {
        //        _audioPlayerService.Play(_audioClip);
        //        ShowActiveState();
        //        _isFirstClick = false;
        //    }
        //    else
        //    {
        //        _audioPlayerService.PlayAfterPause();
        //        ShowActiveState();
        //    }
        //}
        //else
        //{
        //    _audioPlayerService.Pause();
        //    ShowPauseState();
        //}
    }

    public void ShowInactiveState()
    {
        _iconImage.sprite = _pauseSprite;

        Debug.Log("ShowInactiveState()");

        if (AudioClip == null)
        {
            Debug.Log("_audioClip == null");
            Transform parent = this.transform.parent;
            Transform grandParent = parent.parent;
            Debug.Log($"parent = {parent.name}; grandParent = {grandParent.name};");
        }

        _mediaLength.text = TimeSpan.FromSeconds(AudioClip.length).ToString("mm\\:ss");
    }
    
    public void ShowTextInPlay(string mediaCurrentTime, string mediaLength) =>
        _mediaLength.text = $"{mediaCurrentTime}/{mediaLength}";

    public void ShowActiveState() =>
        _iconImage.sprite = _playSprite;
    
    public void ShowPauseState() =>
        _iconImage.sprite = _pauseSprite;

    //private void OnEnable()
    //{
    //    if (_audioPlayerService != null)
    //        _audioPlayerService.AudioEnd += OnAudioEnd;
    //}

    //private void OnDisable()
    //{
    //    if (_audioPlayerService != null)
    //        _audioPlayerService.AudioEnd -= OnAudioEnd;
    //}

    private void OnAudioEnd() =>
        ShowInactiveState();

    //private void Update()
    //{
    //    if (_audioPlayerService.IsPlaying)
    //        _mediaLength.text = $"{_audioPlayerService.MediaCurrentTime}/{_audioPlayerService.MediaLength}";
    //}
}