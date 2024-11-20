using System;
using UnityEngine;

public class AudioPlayerService : MonoBehaviour
{
    public bool IsPlaying => _audioSource.isPlaying;
    public string MediaCurrentTime { get; private set; }
    public string MediaLength { get; private set; }

    private AudioSource _audioSource;
    private CharacterChanger _characterChanger;
    private DefaultPanelSwitcher _defaultPanelSwitcher;
    private float _timer;
    private bool _audioIsEndedFlag;

    private AudioButton _currentAudioButton;
    private AudioClip _audioClip;

    public void Init(CharacterChanger characterChanger, DefaultPanelSwitcher defaultPanelSwitcher)
    {
        _audioSource = GetComponent<AudioSource>();
        _characterChanger = characterChanger;
        _defaultPanelSwitcher = defaultPanelSwitcher;
        _characterChanger.ShowNewCharacter += OnShowNewCharacter;
        ItemIcon.OnClickOnItem += OnShowNewItem;
    }

    private void OnShowNewCharacter(CharacterSo itemSo) =>
        Stop();

    private void OnShowNewItem(CharacterItemSo itemSo)
    {
        Stop();

        if (itemSo.ItemNameAudioClip != null)
            PlayAudioFromEvents(itemSo.ItemNameAudioClip);
    }

    private void PlayAudioFromEvents(AudioClip audioClip)
    {
        Debug.Log("PlayAudioFromEvents");
        _currentAudioButton = null;
        _audioClip = audioClip;
        Play(audioClip);
    }

    public void WorkWithAudioButton(AudioButton audioButton)
    {
        if (_currentAudioButton == audioButton)
        {
            if (_audioSource.isPlaying)
            {
                Pause();
                _currentAudioButton.ShowPauseState();
            }
            else if (_audioClip == audioButton.AudioClip)
            {
                PlayAfterPause();
                _currentAudioButton.ShowActiveState();
            }
            else
            {
                _audioClip = audioButton.AudioClip;
                Play(_audioClip);
                _currentAudioButton.ShowActiveState();
            }
        }
        else
        {
            if (_currentAudioButton != null)
                _currentAudioButton.ShowInactiveState();

            _audioClip = audioButton.AudioClip;
            _currentAudioButton = audioButton;
            _currentAudioButton.ShowActiveState();
            Play(_currentAudioButton.AudioClip);
        }
    }

    public void Play(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
        _defaultPanelSwitcher.StopWatching();
    }

    public void PlayAfterPause()
    {
        _audioSource.Play();
        _defaultPanelSwitcher.StopWatching();
    }

    public void Pause()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Pause();
            _defaultPanelSwitcher.StartWatching();
        }
    }

    public void Stop()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
            _defaultPanelSwitcher.StartWatching();
        }
    }

    private void Update()
    {
        if (IsPlaying)
        {
            float currentTime = _audioSource.time;
            float totalLength = _audioSource.clip.length;
            MediaCurrentTime = TimeSpan.FromSeconds(currentTime).ToString("mm\\:ss");
            MediaLength = TimeSpan.FromSeconds(totalLength).ToString("mm\\:ss");

            if (_currentAudioButton != null)
                _currentAudioButton.ShowTextInPlay(MediaCurrentTime, MediaLength);

            if (Tools.CheckEqualWithThreshold(currentTime, totalLength, 1f))
                _audioIsEndedFlag = true;
        }
        else
        {
            if (_audioIsEndedFlag)
            {
                _audioIsEndedFlag = false;

                if (_currentAudioButton != null)
                    _currentAudioButton.ShowInactiveState();

                _audioClip = null;

                Debug.Log("аудио закончилось");
            }
        }
    }
}