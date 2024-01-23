using UnityEngine;
using UnityEngine.UI;

public class TopButtonsPanel : MonoBehaviour
{
    [SerializeField] private CharacterTopButton[] _characterButtons;
    [SerializeField] private Button _museumButton;
    private CanvasController _canvasController;

    public void Init(CanvasController canvasController)
    {
        _canvasController = canvasController;

        foreach (var button in _characterButtons)
            button.Init(_canvasController);

        _museumButton.onClick.AddListener(() => OnMuseumButtonClick());
    }

    private void OnMuseumButtonClick()
    {
        Debug.Log("Нажали на кнопку музея");
    }
}