using UnityEngine;
using UnityEngine.UI;

public class WinCharacterInfoPanel : MonoBehaviour
{
    [SerializeField] private AudioButton _audioButton;
    [SerializeField] private Text _professionText;
    [SerializeField] private Text _descriptionText;

    public void Init()
    {
        _audioButton.Init();
    }
}
