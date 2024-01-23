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
         
    public void Init()
    {
        _characterChanger = ServiceLocator.Instance.CharacterChanger;
        _characterChanger.ShowNewCharacter += CharacterChanger_ShowNewCharacter;
    }

    private void CharacterChanger_ShowNewCharacter(CharacterSo characterSo)
    {
        _currentCharacter = characterSo;
    }
}