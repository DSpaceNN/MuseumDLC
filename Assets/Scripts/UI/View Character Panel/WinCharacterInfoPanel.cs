using UnityEngine;
using UnityEngine.UI;

public class WinCharacterInfoPanel : MonoBehaviour
{
    [SerializeField] private AudioButton _audioButton;
    [SerializeField] private Text _smallHeaderText;
    [SerializeField] private Text _professionText;
    [SerializeField] private Text _descriptionText;

    public void Init()
    {
        _audioButton.Init();
        ServiceLocator.Instance.CharacterDresser.CharacterIsFullyEquiped += OnCharacterFullEquipe;
    }

    private void OnCharacterFullEquipe()
    {
        var characterSo = ServiceLocator.Instance.CharacterChanger.CurrentCharacter;
        _audioButton.SetContent(characterSo.WinAudioClip);

        _smallHeaderText.text = characterSo.CharacterName;
        _professionText.text = characterSo.CharacterProf;
        _descriptionText.text = characterSo.WinDescription;
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.CharacterDresser.CharacterIsFullyEquiped -= OnCharacterFullEquipe;
    }
}