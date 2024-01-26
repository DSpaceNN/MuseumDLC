using UnityEngine;
using UnityEngine.UI;

public class ViewCharacterInfoPanel : MonoBehaviour
{
    [SerializeField] private Button _audioButton;
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

        _characterChanger.ShowNewCharacter += CharacterChanger_ShowNewCharacter;
        CharacterChanger_ShowNewCharacter(_characterChanger.CurrentCharacter);
        _audioButton.onClick.AddListener(() => OnAudioButtonClick());
    }

    //TODO переместить это всё в отдельный класс кнопки
    private void OnAudioButtonClick()
    {
        Debug.Log("OnAudioButtonClick");
        if (!_audioPlayer.IsPlaying)
        {
            _audioPlayer.Play(_currentCharacter.CharacterTaskAudioClip);
            //сменить спрайт
        }   
        else
        {
            //сменить спрайт
            _audioPlayer.Stop();
        }
    }

    private void CharacterChanger_ShowNewCharacter(CharacterSo characterSo)
    {
        _currentCharacter = characterSo;
        _taskText.text = _currentCharacter.Task;
        _purposeText.text = _currentCharacter.Purpose;
    }
}