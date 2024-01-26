using UnityEngine;

public class AudioPlayerService : MonoBehaviour
{
    public bool IsPlaying => _audioSource.isPlaying;

    private AudioSource _audioSource;
    private CharacterChanger _characterChanger;

    public void Init(CharacterChanger characterChanger)
    {
        _audioSource = GetComponent<AudioSource>();
        _characterChanger = characterChanger;
        _characterChanger.ShowNewCharacter += OnShowNewCharacter;
    }

    private void OnShowNewCharacter(CharacterSo obj) =>
        Stop();

    public void Play(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void Stop()
    {
        if (_audioSource.isPlaying)
            _audioSource.Stop();
    }
}
