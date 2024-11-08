using System;
using UnityEngine;

public class AudioPlayerService : MonoBehaviour
{
    public bool IsPlaying => _audioSource.isPlaying;
    public string MediaCurrentTime { get; private set; }
    public string MediaLength { get; private set; }

    public event Action AudioEnd;

    private AudioSource _audioSource;
    private CharacterChanger _characterChanger;
    private DefaultPanelSwitcher _defaultPanelSwitcher;
    private float _timer;
    private bool _audioIsEndedFlag;

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
            Play(itemSo.ItemNameAudioClip);
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

            if (Tools.CheckEqualWithThreshold(currentTime, totalLength, 1f))
                _audioIsEndedFlag = true;
        }
        else
        {
            if (_audioIsEndedFlag)
            {
                AudioEnd?.Invoke();
                _audioIsEndedFlag = false;
            }   
        }
    }
}