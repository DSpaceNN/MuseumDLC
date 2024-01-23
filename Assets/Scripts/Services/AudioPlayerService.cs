using UnityEngine;

public class AudioPlayerService : MonoBehaviour
{
    private AudioSource _audioSource;

    public void Init()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void StopAudio()
    {
        _audioSource.Stop();
    }
}
